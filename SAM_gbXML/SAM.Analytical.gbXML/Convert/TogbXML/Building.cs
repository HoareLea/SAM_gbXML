using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;
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

            //Dictionary of Minimal Elevations and List of Panels
            Dictionary<double, List<Panel>> dictionary_MinElevations = Analytical.Query.MinElevationDictionary(panels, true, Tolerance.MacroDistance);

            //Dictionary of gbXML BuildingStoreys and its elevations
            Dictionary<BuildingStorey, double> dictionary_buildingStoreys = new Dictionary<BuildingStorey, double>();

            //Dictionary of SAM Panels related buildingSorey, minimal elevation and maximal elevation
            Dictionary<Panel, Tuple<BuildingStorey, double, double, double>> dictionary_Panels = new Dictionary<Panel, Tuple<BuildingStorey, double, double, double>>();

            foreach(KeyValuePair<double, List<Panel>> keyValuePair in dictionary_MinElevations)
            {
                BuildingStorey buildingStorey = Architectural.Create.Level(keyValuePair.Key).TogbXML(tolerance);
                dictionary_buildingStoreys[buildingStorey] = keyValuePair.Key;
                foreach(Panel panel in keyValuePair.Value)
                    dictionary_Panels[panel] = new Tuple<BuildingStorey, double, double, double> (buildingStorey, keyValuePair.Key, panel.MinElevation(), panel.MaxElevation());
            }

            List<gbXMLSerializer.Space> spaces_gbXML = new List<gbXMLSerializer.Space>();
            Dictionary<Guid, SpaceBoundary> dictionary = new Dictionary<Guid, SpaceBoundary>();
            foreach (Space space in spaces)
            {
                List<Panel> panels_Space = adjacencyCluster.GetRelatedObjects<Panel>(space);
                if (panels_Space == null || panels_Space.Count == 0)
                    continue;

                double elevation_Level = panels_Space.ConvertAll(x => dictionary_Panels[x].Item2).Min();
                double elevation_Min = panels_Space.ConvertAll(x => dictionary_Panels[x].Item3).Min();
                double elevation_Max = panels_Space.ConvertAll(x => dictionary_Panels[x].Item4).Max();
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

                if(!space.TryGetValue(SpaceParameter.Volume, out double volume))
                {
                    volume = double.NaN;
                }

                if (!space.TryGetValue(SpaceParameter.Area, out double area))
                {
                    area = double.NaN;
                }

                Face3D face3D = null;

                Shell shell = adjacencyCluster.Shell(space);
                BoundingBox3D boundingBox3D = shell?.GetBoundingBox();
                if (shell != null && boundingBox3D != null)
                {
                    if(double.IsNaN(volume))
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
                            if(double.IsNaN(area))
                            {
                                area = face3Ds.ConvertAll(x => x.GetArea()).Sum();
                            }

                            face3Ds = face3Ds.Union(tolerance);

                            if(face3Ds.Count > 1)
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
                    List<Panel> panels_PlanarGeometry = panels_Space.FindAll(x => x.PanelType.PanelGroup() == PanelGroup.Floor || (x.Normal.AlmostSimilar(Vector3D.WorldZ.GetNegated()) && dictionary_Panels[x].Item3 == elevation_Min));
                    //panels_PlanarGeometry = panels_PlanarGeometry?.MergeCoplanarPanels(Tolerance.MacroDistance, false, false, Tolerance.MacroDistance);
                    if (panels_PlanarGeometry != null || panels_PlanarGeometry.Count != 0)
                    {
                        List<Face3D> face3Ds = panels_PlanarGeometry.ConvertAll(x => x.GetFace3D());
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

                if(face3D == null)
                {
                    continue;
                }

                List<SpaceBoundary> spaceBoundaries = new List<SpaceBoundary>();
                foreach (Panel panel in panels_Space)
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
