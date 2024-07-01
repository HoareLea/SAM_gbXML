using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Object.Spatial;
using SAM.Geometry.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Define static class containing extension methods for AdjacencyCluster
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts AdjacencyCluster to gbXML Building
        /// </summary>
        /// <param name="adjacencyCluster">AdjacencyCluster</param>
        /// <param name="name">Name of gbXML Building</param>
        /// <param name="description">Description of gbXML Building</param>
        /// <param name="tolerance">Tolerance</param>
        /// <returns>gbXML Building</returns>
        public static Building TogbXML(this AdjacencyCluster adjacencyCluster, string name, string description, double tolerance = Tolerance.MicroDistance)
        {
            // Get all panels from the adjacency cluster
            List<IPanel> panels = adjacencyCluster?.GetObjects<IPanel>();

            // Return null if there are no panels
            if (panels == null || panels.Count == 0)
                return null;

            // Get all spaces from the adjacency cluster
            List<ISpace> spaces = adjacencyCluster.GetObjects<ISpace>();

            // Return null if there are no spaces
            if (spaces == null)
                return null;

            //Dictionary of Minimal Elevations and List of Panels
            // Create a dictionary of minimal elevations and the panels that lie at that elevation
            Dictionary<double, List<IPanel>> dictionary_MinElevations = Analytical.Query.MinElevationDictionary(panels, true, Tolerance.MacroDistance);

            //Dictionary of gbXML BuildingStoreys and its elevations
            // Create a dictionary to store the building storeys and their elevations
            Dictionary<BuildingStorey, double> dictionary_buildingStoreys = new();

            //Dictionary of SAM Panels related buildingSorey, minimal elevation and maximal elevation
            // Create a dictionary to store the relationship between panels, building storeys, and their elevations
            Dictionary<IPanel, Tuple<BuildingStorey, double, double, double>> dictionary_Panels = new();

            // Iterate through each elevation in the dictionary
            foreach (KeyValuePair<double, List<IPanel>> keyValuePair in dictionary_MinElevations)
            {
                // Create a new building storey for the elevation and add it to the dictionary
                BuildingStorey buildingStorey = Architectural.Create.Level(keyValuePair.Key).TogbXML(tolerance);
                dictionary_buildingStoreys[buildingStorey] = keyValuePair.Key;

                // Iterate through each panel at the current elevation
                foreach (Panel panel in keyValuePair.Value)
                    // Add the panel to the dictionary with the associated building storey, minimal elevation, and maximal elevation
                    dictionary_Panels[panel] = new Tuple<BuildingStorey, double, double, double>(buildingStorey, keyValuePair.Key, panel.MinElevation(), panel.MaxElevation());
            }

            // Create a list to store the gbXML spaces
            List<gbXMLSerializer.Space> spaces_gbXML = new();

            // Create a dictionary to store the relationship between space boundaries and their IDs
            Dictionary<Guid, SpaceBoundary> dictionary = new();

            // Iterate through each space in the adjacency cluster
            foreach (ISpace space in spaces)
            {
                List<IPanel> panels_Space = adjacencyCluster.GetRelatedObjects<IPanel>(space);
                if (panels_Space == null || panels_Space.Count == 0)
                {
                    continue;
                }

                double elevation_Level = panels_Space.ConvertAll(x => dictionary_Panels[x].Item2).Min();
                double elevation_Min = panels_Space.ConvertAll(x => dictionary_Panels[x].Item3).Min();
                double elevation_Max = panels_Space.ConvertAll(x => dictionary_Panels[x].Item4).Max();
                BuildingStorey buildingStorey = null;

                // Finding BuildingStorey based on Elevation level of the panels in the Space
                foreach (KeyValuePair<BuildingStorey, double> keyValuePair in dictionary_buildingStoreys)
                {
                    if (keyValuePair.Value.Equals(elevation_Level))
                    {
                        buildingStorey = keyValuePair.Key;
                        break;
                    }
                }

                if (buildingStorey == null)
                {
                    continue;
                }

                double volume = double.NaN;
                double area = double.NaN;

                if (space is IParameterizedSAMObject)
                {
                    if (!((IParameterizedSAMObject)space).TryGetValue(Analytical.SpaceParameter.Volume, out volume))
                    {
                        volume = double.NaN;
                    }

                    if (!((IParameterizedSAMObject)space).TryGetValue(Analytical.SpaceParameter.Area, out area))
                    {
                        area = double.NaN;
                    }
                }

                Face3D face3D = null;

                Shell shell = adjacencyCluster.Shell(space);
                BoundingBox3D boundingBox3D = shell?.GetBoundingBox();
                if (shell != null && boundingBox3D != null)
                {
                    if (double.IsNaN(volume))
                    {
                        volume = shell.Volume(tolerance: tolerance);
                    }

                    double offset = (boundingBox3D.Max.Z - boundingBox3D.Min.Z) / 2;

                    List<Face3D> face3Ds = shell.Section(offset, false, Tolerance.Angle, tolerance, Tolerance.MacroDistance);
                    if (face3Ds != null)
                    {
                        face3Ds.RemoveAll(x => x == null || x.GetArea() < Tolerance.MacroDistance);
                        if (face3Ds.Count != 0)
                        {
                            if (double.IsNaN(area))
                            {
                                area = face3Ds.ConvertAll(x => x.GetArea()).Sum();
                            }

                            face3Ds = face3Ds.Union(tolerance);

                            if (face3Ds.Count > 1)
                            {
                                face3Ds.Sort((x, y) => y.GetArea().CompareTo(x.GetArea()));
                            }

                            face3D = face3Ds[0];
                            face3D = face3D.GetMoved(new Vector3D(0, 0, -offset)) as Face3D;
                        }
                    }
                }

                if (double.IsNaN(area) || face3D == null)
                {
                    List<IPanel> panels_PlanarGeometry = panels_Space.FindAll(x => (x is Panel && ((Panel)x).PanelType.PanelGroup() == PanelGroup.Floor) || (Vector3D.WorldZ.GetNegated().AlmostSimilar(x?.Face3D?.GetPlane()?.Normal) && dictionary_Panels[x].Item3 == elevation_Min));
                    //panels_PlanarGeometry = panels_PlanarGeometry?.MergeCoplanarPanels(Tolerance.MacroDistance, false, false, Tolerance.MacroDistance);
                    if (panels_PlanarGeometry != null || panels_PlanarGeometry.Count != 0)
                    {
                        List<Face3D> face3Ds = panels_PlanarGeometry.ConvertAll(x => x.Face3D);
                        if (face3Ds != null)
                        {
                            face3Ds.RemoveAll(x => x == null || x.GetArea() < Tolerance.MacroDistance);
                            if (face3Ds.Count != 0)
                            {
                                Plane plane = Geometry.Spatial.Create.Plane(elevation_Min);
                                List<Geometry.Planar.Face2D> face2Ds = face3Ds.ConvertAll(x => plane.Convert(plane.Project(x)));
                                face2Ds.RemoveAll(x => x == null || x.GetArea() < Tolerance.MacroDistance);
                                if (face2Ds.Count != 0)
                                {
                                    face2Ds = Geometry.Planar.Query.Union(face2Ds);
                                }

                                if (double.IsNaN(area))
                                {
                                    area = face3Ds.ConvertAll(x => x.GetArea()).Sum();
                                }

                                if (face2Ds.Count > 1)
                                {
                                    face2Ds.Sort((x, y) => y.GetArea().CompareTo(x.GetArea()));
                                }

                                face3D = plane.Convert(face2Ds[0]);
                            }
                        }
                    }

                }

                if (double.IsNaN(area))
                {
                    continue;
                }

                if (double.IsNaN(volume))
                {
                    volume = boundingBox3D == null ? Math.Abs(elevation_Max - elevation_Min) * area : (boundingBox3D.Max.Z - boundingBox3D.Min.Z) * area;
                }

                if (double.IsNaN(area) || area < Tolerance.MacroDistance)
                {
                    continue;
                }

                if (double.IsNaN(volume) || volume < Tolerance.MacroDistance)
                {
                    continue;
                }

                if (face3D == null)
                {
                    continue;
                }

                List<SpaceBoundary> spaceBoundaries = new();
                foreach (IPanel panel in panels_Space)
                {
                    if (panel == null)
                    {
                        continue;
                    }

                    SpaceBoundary spaceBoundary = new();
                    if (!dictionary.TryGetValue(panel.Guid, out spaceBoundary))
                    {
                        spaceBoundary = panel.TogbXML_SpaceBoundary(tolerance);
                        dictionary[panel.Guid] = spaceBoundary;
                    }

                    spaceBoundaries.Add(spaceBoundary);
                }

                gbXMLSerializer.Space space_gbXML = new();
                space_gbXML.Name = space.Name;
                space_gbXML.spacearea = new Area() { val = area.ToString() };
                space_gbXML.spacevol = new Volume() { val = volume.ToString() };
                space_gbXML.buildingStoreyIdRef = buildingStorey.id;
                space_gbXML.cadid = new CADObjectId() { id = space.Guid.ToString() };
                space_gbXML.PlanarGeo = face3D.TogbXML(tolerance);
                space_gbXML.id = Core.gbXML.Query.Id(space, typeof(gbXMLSerializer.Space));
                space_gbXML.spbound = spaceBoundaries.ToArray();
                space_gbXML.ShellGeo = panels_Space.TogbXML(space, tolerance);

                spaces_gbXML.Add(space_gbXML);
            }

            Building building = new();
            building.id = Core.gbXML.Query.Id(adjacencyCluster, typeof(Building));
            building.Name = name;
            building.Description = description;
            building.bldgStories = dictionary_buildingStoreys.Keys.ToArray();
            building.Area = Analytical.Query.Area(panels.FindAll(x => x is Panel).ConvertAll(x => (Panel)x), PanelGroup.Floor);
            building.buildingType = buildingTypeEnum.Office;
            building.Spaces = spaces_gbXML.ToArray();

            return building;
        }
    }
}
