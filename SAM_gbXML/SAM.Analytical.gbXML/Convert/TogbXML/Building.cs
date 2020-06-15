using gbXMLSerializer;
using SAM.Architectural;
using SAM.Core;
using SAM.Geometry.gbXML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Building TogbXML(this AdjacencyCluster adjacencyCluster, string name, string description, double tolerance = Tolerance.MicroDistance)
        {
            List<Panel> panels = adjacencyCluster?.GetPanels();
            if (panels == null || panels.Count == 0)
                return null;

            List<Space> spaces = adjacencyCluster.GetSpaces();
            if (spaces == null)
                return null;

            List<Panel> panels_Floors = panels.FindAll(x => Analytical.Query.PanelGroup(x.PanelType) == PanelGroup.Floor);

            //Dictionary of Minimal Elevations and List of Panels
            Dictionary<double, List<Panel>> dictionary_MinElevations = Analytical.Query.MinElevationDictionary(panels_Floors, Tolerance.MacroDistance);
            List<double> elevations = dictionary_MinElevations.Keys.ToList();

            foreach(Panel panel in panels)
            {
                if (panels_Floors.Contains(panel))
                    continue;

                double elevation = panel.MinElevation();

                int index = elevations.IndexOf(elevations.ConvertAll(x => Math.Abs(x - elevation)).Min());
                dictionary_MinElevations.Values.ElementAt(index).Add(panel);
            }

            //Dictionary of gbXML BuildingStoreys and its elevations
            Dictionary<BuildingStorey, double> dictionary_buildingStoreys = new Dictionary<BuildingStorey, double>();

            //Dictionary of SAM Panels related buildingSorey, minimal elevation and maximal elevation
            Dictionary<Panel, Tuple<BuildingStorey, double, double>> dictionary_Panels = new Dictionary<Panel, Tuple<BuildingStorey, double, double>>();

            foreach(KeyValuePair<double, List<Panel>> keyValuePair in dictionary_MinElevations)
            {
                BuildingStorey buildingStorey = Architectural.Create.Level(keyValuePair.Key).TogbXML(tolerance);
                dictionary_buildingStoreys[buildingStorey] = keyValuePair.Key;
                foreach(Panel panel in keyValuePair.Value)
                    dictionary_Panels[panel] = new Tuple<BuildingStorey, double, double> (buildingStorey, keyValuePair.Key, panel.MaxElevation());
            }

            List<gbXMLSerializer.Space> spaces_gbXML = new List<gbXMLSerializer.Space>();
            foreach (Space space in spaces)
            {
                List<Panel> panels_Space = adjacencyCluster.GetRelatedObjects<Panel>(space);
                if (panels_Space == null || panels_Space.Count == 0)
                    continue;

                double elevation_Min = panels_Space.ConvertAll(x => dictionary_Panels[x].Item2).Min();
                double elevation_Max = panels_Space.ConvertAll(x => dictionary_Panels[x].Item3).Max();
                BuildingStorey buildingStorey = null;
                foreach (KeyValuePair<BuildingStorey, double> keyValuePair in dictionary_buildingStoreys)
                {
                    if (keyValuePair.Value.Equals(elevation_Min))
                    {
                        buildingStorey = keyValuePair.Key;
                        break;
                    }
                }

                if (buildingStorey == null)
                    continue;

                List<Panel> panels_PlanarGeometry = panels_Space.FindAll(x => x.PanelType.PanelGroup() == PanelGroup.Floor);
                panels_PlanarGeometry = panels_PlanarGeometry?.MergeCoplanarPanels(Core.Tolerance.MacroDistance, false, false, Core.Tolerance.MacroDistance);
                if (panels_PlanarGeometry == null || panels_PlanarGeometry.Count == 0)
                    continue;

                panels_PlanarGeometry.Sort((x, y) => y.GetArea().CompareTo(x.GetArea()));

                Geometry.Spatial.Face3D face3D = panels_PlanarGeometry.First().PlanarBoundary3D?.GetFace3D();
                if (face3D == null)
                    continue;

                double area = face3D.GetArea();
                if (area < Tolerance.MacroDistance)
                    continue;

                double volume = Math.Abs(elevation_Max - elevation_Min) * area;
                if (volume < Tolerance.MacroDistance)
                    continue;

                gbXMLSerializer.Space space_gbXML = new gbXMLSerializer.Space();
                space_gbXML.Name = space.Name;
                space_gbXML.spacearea = new Area() { val = area.ToString() };
                space_gbXML.spacevol = new Volume() { val = volume.ToString() };
                space_gbXML.buildingStoreyIdRef = buildingStorey.id;
                space_gbXML.cadid = new CADObjectId() { id = space.Guid.ToString() };
                space_gbXML.PlanarGeo = face3D.TogbXML(tolerance);
                space_gbXML.id = Core.gbXML.Query.Id(space, typeof(gbXMLSerializer.Space));
                space_gbXML.spbound = panels_Space.ConvertAll(x => x.TogbXML_SpaceBoundary(tolerance)).ToArray();
                space_gbXML.ShellGeo = panels_Space.TogbXML(space, tolerance);

                spaces_gbXML.Add(space_gbXML);
            }

            Building building = new Building();
            building.id = Core.gbXML.Query.Id(adjacencyCluster, typeof(Building));
            building.Name = name;
            building.Description = description;
            building.bldgStories = dictionary_buildingStoreys.Keys.ToArray();
            building.Area = Analytical.Query.Area(panels, PanelGroup.Floor);
            building.buildingType = buildingTypeEnum.Office;
            building.Spaces = spaces_gbXML.ToArray();

            return building;
        }
    }
}
