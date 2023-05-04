using gbXMLSerializer;
using SAM.Core;
using SAM.Core.gbXML;
using System.Collections.Generic;

// Creating a namespace to hold our extension method
namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts gbXML data into a BuildingModel object of the SAM library.
        /// </summary>
        /// <param name="gbXML">The gbXML data to be converted.</param>
        /// <param name="silverSpacing">The minimum distance between surfaces in silver.</param>
        /// <param name="tolerance">The tolerance for comparing double values.</param>
        /// <returns>A BuildingModel object of the SAM library.</returns>
        public static BuildingModel ToSAM_BuildingModel(this gbXMLSerializer.gbXML gbXML, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.Distance)
        {
            if (gbXML == null)
                return null;

            // Converting campus data to BuildingModel
            return ToSAM_BuildingModel(gbXML.Campus, silverSpacing, tolerance);
        }

        /// <summary>
        /// Converts Campus data into a BuildingModel object of the SAM library.
        /// </summary>
        /// <param name="campus">The Campus data to be converted.</param>
        /// <param name="silverSpacing">The minimum distance between surfaces in silver.</param>
        /// <param name="tolerance">The tolerance for comparing double values.</param>
        /// <returns>A BuildingModel object of the SAM library.</returns>
        public static BuildingModel ToSAM_BuildingModel(this Campus campus, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.Distance)
        {
            // Converting location data into Address and Core.Location objects
            Address address = campus.Location.ToSAM_Address();
            Core.Location location = campus.Location.ToSAM(tolerance);

            // Creating a new BuildingModel object
            BuildingModel result = new BuildingModel(
                null, // Building name is not specified
                location,
                address,
                Architectural.Create.PlanarTerrain(0), // Elevation is set to 0
                new MaterialLibrary(string.Empty), // Empty material library is created
                new ProfileLibrary(string.Empty) // Empty profile library is created
            );

            // Dictionary to store Space objects with their respective ids
            Dictionary<string, Space> dictionary_Space = new Dictionary<string, Space>();

            Building[] buildings = campus.Buildings;
            if (buildings != null)
            {
                // Looping through each building in the campus
                foreach (Building building in buildings)
                {
                    gbXMLSerializer.Space[] spaces = building.Spaces;
                    if (spaces != null)
                    {
                        // Looping through each space in the building
                        foreach (gbXMLSerializer.Space space_gbXML in spaces)
                        {
                            if (space_gbXML == null)
                                continue;

                            // Converting gbXML Space data to Space object of the SAM library
                            Space space_SAM = space_gbXML.ToSAM(silverSpacing, tolerance);
                            if (space_SAM == null)
                                continue;

                            // Adding Space object to the dictionary
                            dictionary_Space[space_gbXML.id] = space_SAM;

                            // Adding Space object to the BuildingModel
                            result.Add(space_SAM);
                        }
                    }
                }
            }

            Surface[] surfaces = campus.Surface;
            if (surfaces != null)
            {
                // Looping through each surface in the campus
                foreach (Surface surface in surfaces)
                {
                    if (surface == null)
                        continue;

                    IPartition partition = surface.ToSAM_Partition(tolerance);
                    if (partition == null)
                        continue;

                    result.Add(partition);

                    AdjacentSpaceId[] adjacentSpaceIds = surface.AdjacentSpaceId;
                    if (adjacentSpaceIds == null || adjacentSpaceIds.Length == 0)
                        continue;

                    foreach(AdjacentSpaceId adjacentSpaceId in adjacentSpaceIds)
                    {
                        string id = adjacentSpaceId?.spaceIdRef;
                        if (string.IsNullOrWhiteSpace(id))
                            continue;

                        Space space = null;

                        if (!dictionary_Space.TryGetValue(id, out space))
                            continue;

                        result.AddRelation(partition, space);
                    }

                }
            }

            return result;
        }

    }
}
