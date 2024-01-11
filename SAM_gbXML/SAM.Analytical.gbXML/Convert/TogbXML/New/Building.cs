using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;
using SAM.Geometry.Object.Spatial;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// This class contains methods to convert SAM materials to gbXML materials.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts BuildingModel to gbXML Building.
        /// </summary>
        /// <param name="buildingModel">BuildingModel to convert.</param>
        /// <param name="name">Name of the Building.</param>
        /// <param name="description">Description of the Building.</param>
        /// <param name="tolerance">Tolerance for conversion. Default value is Tolerance.MicroDistance.</param>
        /// <returns>gbXML Building.</returns>
        public static Building TogbXML(this BuildingModel buildingModel, string name, string description, double tolerance = Tolerance.MicroDistance)
        {
            List<IPartition> partitions = buildingModel?.GetPartitions();
            if (partitions == null || partitions.Count == 0)
                return null;

            List<Space> spaces = buildingModel.GetSpaces();
            if (spaces == null)
                return null;

            //Dictionary of Minimal Elevations and List of Panels
            Dictionary<double, List<IPartition>> dictionary_MinElevations = Geometry.Object.Spatial.Query.ElevationDictionary(partitions, Tolerance.MacroDistance);

            //Dictionary of gbXML BuildingStoreys and its elevations
            Dictionary<BuildingStorey, double> dictionary_buildingStoreys = new Dictionary<BuildingStorey, double>();

            //Dictionary of SAM Panels related buildingSorey, minimal elevation and maximal elevation
            Dictionary<IPartition, Tuple<BuildingStorey, double, double, double>> dictionary_Partitions = new Dictionary<IPartition, Tuple<BuildingStorey, double, double, double>>();

            foreach(KeyValuePair<double, List<IPartition>> keyValuePair in dictionary_MinElevations)
            {
                BuildingStorey buildingStorey = Architectural.Create.Level(keyValuePair.Key).TogbXML(tolerance);
                dictionary_buildingStoreys[buildingStorey] = keyValuePair.Key;
                foreach(IPartition partition in keyValuePair.Value)
                    dictionary_Partitions[partition] = new Tuple<BuildingStorey, double, double, double> (buildingStorey, keyValuePair.Key, partition.MinElevation(), partition.MaxElevation());
            }

            double buildingArea = 0;

            List<gbXMLSerializer.Space> spaces_gbXML = new List<gbXMLSerializer.Space>();
            Dictionary<Guid, SpaceBoundary> dictionary = new Dictionary<Guid, SpaceBoundary>();
            foreach (Space space in spaces)
            {
                List<IPartition> partitions_Space = buildingModel.GetPartitions(space);
                if (partitions_Space == null || partitions_Space.Count == 0)
                    continue;

                double elevation_Level = partitions_Space.ConvertAll(x => dictionary_Partitions[x].Item2).Min();
                double elevation_Min = partitions_Space.ConvertAll(x => dictionary_Partitions[x].Item3).Min();
                double elevation_Max = partitions_Space.ConvertAll(x => dictionary_Partitions[x].Item4).Max();
                BuildingStorey buildingStorey = null;
                foreach (KeyValuePair<BuildingStorey, double> keyValuePair in dictionary_buildingStoreys)
                {
                    if (keyValuePair.Value.Equals(elevation_Level))
                    {
                        buildingStorey = keyValuePair.Key;
                        break;
                    }
                }

                if (buildingStorey == null)
                    continue;

                List<Face3D> face3Ds = null;

                Shell shell = buildingModel.GetShell(space);
                if(shell != null)
                {
                    face3Ds = shell.Section(Tolerance.MacroDistance, false, Tolerance.Angle, tolerance, Tolerance.MacroDistance);
                    face3Ds?.RemoveAll(x => x == null || x.GetArea() < Tolerance.MacroDistance);
                    face3Ds = face3Ds.ConvertAll(x => x.GetMoved(new Vector3D(0, 0, - Tolerance.MacroDistance)) as Face3D);
                }

                if(face3Ds == null || face3Ds.Count == 0)
                {
                    List<IPartition> partitions_PlanarGeometry = partitions_Space.FindAll(x => x is Floor || (x.Face3D.GetPlane().Normal.AlmostSimilar(Vector3D.WorldZ.GetNegated()) && dictionary_Partitions[x].Item3 == elevation_Min));
                    partitions_PlanarGeometry = partitions_PlanarGeometry.MergeCoplanar(Tolerance.MacroDistance, false, Tolerance.MacroDistance, tolerance);
                    if (partitions_PlanarGeometry != null || partitions_PlanarGeometry.Count != 0)
                    {
                        face3Ds = partitions_PlanarGeometry.ConvertAll(x => x.Face3D);
                        face3Ds?.RemoveAll(x => x == null || x.GetArea() < Tolerance.MacroDistance);
                    }
                }

                if (face3Ds == null || face3Ds.Count == 0)
                {
                    continue;
                }

                if(face3Ds.Count > 1)
                {
                    face3Ds.Sort((x, y) => y.GetArea().CompareTo(x.GetArea()));
                }

                Face3D face3D = face3Ds[0];
                if (face3D == null)
                    continue;

                double area = face3D.GetArea();
                if (area < Tolerance.MacroDistance)
                    continue;

                double volume = Math.Abs(elevation_Max - elevation_Min) * area;
                if (volume < Tolerance.MacroDistance)
                    continue;

                List<SpaceBoundary> spaceBoundaries = new List<SpaceBoundary>();
                foreach (Panel panel in partitions_Space)
                {
                    if (panel == null)
                        continue;

                    SpaceBoundary spaceBoundary = null;
                    if(!dictionary.TryGetValue(panel.Guid, out spaceBoundary))
                    {
                        spaceBoundary = panel.TogbXML_SpaceBoundary(tolerance);
                        dictionary[panel.Guid] = spaceBoundary;
                    }

                    spaceBoundaries.Add(spaceBoundary);
                }

                gbXMLSerializer.Space space_gbXML = new gbXMLSerializer.Space();
                space_gbXML.Name = space.Name;
                space_gbXML.spacearea = new Area() { val = area.ToString() };
                space_gbXML.spacevol = new Volume() { val = volume.ToString() };
                space_gbXML.buildingStoreyIdRef = buildingStorey.id;
                space_gbXML.cadid = new CADObjectId() { id = space.Guid.ToString() };
                space_gbXML.PlanarGeo = face3D.TogbXML(tolerance);
                space_gbXML.id = Core.gbXML.Query.Id(space, typeof(gbXMLSerializer.Space));
                space_gbXML.spbound = spaceBoundaries.ToArray();
                space_gbXML.ShellGeo = buildingModel.TogbXML(space, tolerance);// Geometry.gbXML.Convert.TogbXML(shell, tolerance); //partitions_Space.TogbXML(space, tolerance);

                spaces_gbXML.Add(space_gbXML);

                buildingArea += area;
            }

            Building building = new Building();
            building.id = Core.gbXML.Query.Id(buildingModel, typeof(Building));
            building.Name = name;
            building.Description = description;
            building.bldgStories = dictionary_buildingStoreys.Keys.ToArray();
            building.Area = buildingArea;
            building.buildingType = buildingTypeEnum.Office;
            building.Spaces = spaces_gbXML.ToArray();

            return building;
        }
    }
}
