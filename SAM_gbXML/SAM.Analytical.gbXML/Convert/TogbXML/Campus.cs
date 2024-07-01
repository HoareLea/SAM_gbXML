using gbXMLSerializer;
using System.Collections.Generic;
using System.Linq;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A static class that contains extension methods for converting
    /// an AnalyticalModel object to a gbXML file representation.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts an AnalyticalModel object to a Campus object in a gbXML file.
        /// The Campus object represents the entire site.
        /// The silverSpacing parameter is used for calculating the normals of surfaces.
        /// The tolerance parameter is used for comparing double values.
        /// </summary>
        /// <param name="analyticalModel">The AnalyticalModel object to convert.</param>
        /// <param name="silverSpacing">The silverSpacing parameter used for calculating the normals of surfaces. Default is Core.Tolerance.MacroDistance.</param>
        /// <param name="tolerance">The tolerance parameter used for comparing double values. Default is Core.Tolerance.Distance.</param>
        /// <returns>A Campus object representing the site in a gbXML file.</returns>
        public static Campus TogbXML_Campus(this AnalyticalModel analyticalModel, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.Distance)
        {
            if (analyticalModel == null)
                return null;

            //// Create a new Campus object to represent the site.
            //Campus campus = new Campus();

            //// Set the id of the campus using the Query class from the gbXML library.
            //campus.id = Core.gbXML.Query.Id(analyticalModel, typeof(Campus));

            //// Convert the location and address of the analyticalModel to a Location object
            //// in the gbXML file format and assign it to the Location property of the campus object.
            //campus.Location = Core.gbXML.Convert.TogbXML(analyticalModel.Location, analyticalModel.Address, tolerance);

            // Create a new Campus object to represent the site.
            Campus campus = new()
            {
                id = Core.gbXML.Query.Id(analyticalModel, typeof(Campus)),
                Location = Core.gbXML.Convert.TogbXML(analyticalModel.Location, analyticalModel.Address, tolerance)
            };

            // Get the adjacency cluster of the analyticalModel.
            AdjacencyCluster adjacencyCluster = analyticalModel.AdjacencyCluster;

            // If the adjacency cluster exists, perform various operations on it to create the Buildings and Surface objects.
            if (adjacencyCluster != null)
            {
                // Split the adjacency cluster by internal edges to create a simpler representation of the geometry.
                adjacencyCluster = adjacencyCluster.SplitByInternalEdges(tolerance);

                // Update the normals of the adjacency cluster to ensure they are pointing outwards.
                adjacencyCluster = adjacencyCluster.UpdateNormals(true, true, false, silverSpacing, tolerance);

                // Fix any invalid edges in the adjacency cluster to create a valid geometry.
                adjacencyCluster = adjacencyCluster.FixEdges(false, tolerance);

                // Create a Building object representing the entire adjacency cluster.
                // The ToGbXML method is an extension method defined in the AdjacencyClusterExtension class
                // of the gbXMLSerializer library.
                campus.Buildings = new Building[] { adjacencyCluster.TogbXML(analyticalModel.Name, analyticalModel.Description, tolerance) };

                // Get the list of Panel objects in the adjacency cluster.
                List<IPanel> panels = adjacencyCluster.GetObjects<IPanel>();

                if (panels != null)
                {
                    int count_opening = 1;
                    List<Surface> surfaces = new();

                    // Loop through each panel and create a Surface object representing it.
                    for (int i = 0; i < panels.Count; i++)
                    {
                        IPanel panel = panels[i];
                        if (panel == null)
                            continue;

                        // Get the list of Space objects that are adjacent to the panel.
                        List<ISpace> spaces = adjacencyCluster.GetRelatedObjects<ISpace>(panel);

                        if (spaces != null && spaces.Count > 1)
                        {
                            //Spaces have to be in correct order!
                            //https://www.gbxml.org/schema_doc/6.01/GreenBuildingXML_Ver6.01.html#Link7

                            // Sort the list of Space objects in the correct order.
                            SortedDictionary<int, ISpace> sortedDictionary = new();
                            foreach (var space in spaces)
                            {
                                sortedDictionary[adjacencyCluster.GetIndex(space)] = space;
                            }
                            spaces = sortedDictionary.Values.ToList();
                        }

                        // Convert the Panel to a gbXML Surface object and add it to the list of surfaces
                        Surface surface = panel.TogbXML(spaces, i + 1, count_opening, tolerance);
                        if (surface != null)
                            surfaces.Add(surface);

                        // If the Surface has openings, add the number of openings to the count
                        if (surface.Opening != null)
                            count_opening += surface.Opening.Length;

                    }
                    // Convert the list of surfaces to an array and assign it to the Campus
                    campus.Surface = surfaces.ToArray();
                }
            }

            return campus;
        }

    }
}