using gbXMLSerializer;
using SAM.Core;
using SAM.Core.gbXML;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Convert class containing extension methods for converting gbXML objects to SAM objects
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Convert gbXML object to AnalyticalModel object in SAM
        /// </summary>
        /// <param name="gbXML">gbXML object to be converted</param>
        /// <param name="silverSpacing">Silver spacing value (default is Tolerance.MacroDistance)</param>
        /// <param name="tolerance">Tolerance value (default is Tolerance.Distance)</param>
        /// <returns>An AnalyticalModel object in SAM</returns>
        public static AnalyticalModel ToSAM(this gbXMLSerializer.gbXML gbXML, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.Distance)
        {
            if (gbXML == null)
            {
                return null;
            }

            MaterialLibrary materialLibrary = ToSAM_MaterialLibrary(gbXML);

            List<Construction> constructions = ToSAM_Constructions(gbXML);

            AnalyticalModel result = ToSAM(gbXML.Campus, constructions, silverSpacing, tolerance);
            result = new AnalyticalModel(result, result.AdjacencyCluster, materialLibrary, result.ProfileLibrary);

            return result;
        }

        /// <summary>
        /// Convert Campus object to AnalyticalModel object in SAM
        /// </summary>
        /// <param name="campus">Campus object to be converted</param>
        /// <param name="constructions">List of Construction objects (default is null)</param>
        /// <param name="silverSpacing">Silver spacing value (default is Tolerance.MacroDistance)</param>
        /// <param name="tolerance">Tolerance value (default is Tolerance.Distance)</param>
        /// <returns>An AnalyticalModel object in SAM</returns>
        public static AnalyticalModel ToSAM(this Campus campus, IEnumerable<Construction> constructions = null, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.Distance)
        {
            // Convert the campus location to a SAM address and location
            Address address = campus.Location.ToSAM_Address();
            Core.Location location = campus.Location.ToSAM(tolerance);

            // Create a new adjacency cluster
            AdjacencyCluster adjacencyCluster = new AdjacencyCluster();

            // Create a dictionary to store spaces by their IDs
            Dictionary<string, Space> dictionary_Space = new Dictionary<string, Space>();

            // Get all buildings in the campus and loop through them
            Building[] buildings = campus.Buildings;
            if (buildings != null)
            {
                foreach (Building building in buildings)
                {
                    // Get all spaces in the building and loop through them
                    gbXMLSerializer.Space[] spaces = building.Spaces;
                    if (spaces != null)
                    {
                        foreach (gbXMLSerializer.Space space_gbXML in spaces)
                        {
                            // Skip any null spaces
                            if (space_gbXML == null)
                                continue;

                            // Convert the gbXML space to a SAM space and add it to the dictionary and adjacency cluster
                            Space space_SAM = space_gbXML.ToSAM(silverSpacing, tolerance);
                            if (space_SAM == null)
                                continue;

                            dictionary_Space[space_gbXML.id] = space_SAM;
                            adjacencyCluster.AddObject(space_SAM);
                        }
                    }
                }
            }

            // Get all surfaces in the campus and loop through them
            Surface[] surfaces = campus.Surface;
            if (surfaces != null)
            {
                foreach (Surface surface in surfaces)
                {
                    // Skip any null surfaces
                    if (surface == null)
                        continue;

                    // Convert the surface to a SAM panel and add it to the adjacency cluster
                    Panel panel = surface.ToSAM(constructions, tolerance);
                    if (panel == null)
                        continue;

                    adjacencyCluster.AddObject(panel);

                    // Get the adjacent spaces for the surface and loop through them
                    AdjacentSpaceId[] adjacentSpaceIds = surface.AdjacentSpaceId;
                    if (adjacentSpaceIds == null || adjacentSpaceIds.Length == 0)
                        continue;

                    foreach (AdjacentSpaceId adjacentSpaceId in adjacentSpaceIds)
                    {
                        // Get the ID of the adjacent space
                        string id = adjacentSpaceId?.spaceIdRef;
                        if (string.IsNullOrWhiteSpace(id))
                            continue;

                        Space space = null;

                        // If the adjacent space is not in the dictionary, mark the panel as adiabatic and continue to the next adjacent space
                        if (!dictionary_Space.TryGetValue(id, out space))
                        {
                            panel.SetValue(Analytical.PanelParameter.Adiabatic, true);
                            continue;
                        }

                        // Add a relation between the panel and the adjacent space to the adjacency cluster
                        adjacencyCluster.AddRelation(panel, space);
                    }
                }
            }

            // Create a new analytical model with the location, address, and adjacency cluster and return it
            AnalyticalModel result = new AnalyticalModel(string.Empty, string.Empty, location, address, adjacencyCluster);
            return result;
        }

    }
}
