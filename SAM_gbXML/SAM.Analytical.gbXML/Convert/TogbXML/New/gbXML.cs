using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    // Define a static class named 'Convert' within the namespace 'SAM.Analytical.gbXML'
    public static partial class Convert
    {
        /// <summary>
        /// Converts a BuildingModel object to a gbXML object.
        /// </summary>
        /// <param name="buildingModel">The BuildingModel object to convert.</param>
        /// <param name="silverSpacing">The tolerance for silver macro distance (optional, defaults to Core.Tolerance.MacroDistance).</param>
        /// <param name="tolerance_Angle">The tolerance for angle (optional, defaults to Core.Tolerance.Angle).</param>
        /// <param name="tolerance_Distance">The tolerance for distance (optional, defaults to Core.Tolerance.MicroDistance).</param>
        /// <returns>A gbXML object representing the BuildingModel.</returns>
        public static gbXMLSerializer.gbXML TogbXML(this BuildingModel buildingModel, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.MicroDistance)
        {
            // If buildingModel is null, return null
            if (buildingModel == null)
                return null;

            // Create a list to hold constructions
            List<gbXMLSerializer.Construction> constructions = new List<gbXMLSerializer.Construction>();

            // Get the host partition types from the BuildingModel
            List<HostPartitionType> hostPartitionTypes = buildingModel.GetHostPartitionTypes();

            // If there are host partition types
            if (hostPartitionTypes != null && hostPartitionTypes.Count != 0)
            {
                // Loop through the host partition types
                foreach (HostPartitionType hostPartitionType in hostPartitionTypes)
                {
                    // Convert the host partition type to a gbXML construction object
                    gbXMLSerializer.Construction construction = hostPartitionType.TogbXML();

                    // If the conversion was successful
                    if (construction != null)
                    {
                        // Add the construction to the list of constructions
                        constructions.Add(construction);
                    }
                }
            }

            // Get the opening types from the BuildingModel
            List<OpeningType> openiengTypes = buildingModel.GetOpeningTypes();

            // If there are opening types
            if (openiengTypes != null && openiengTypes.Count != 0)
            {
                // Loop through the opening types
                foreach (OpeningType openingType in openiengTypes)
                {
                    // Convert the opening type to a gbXML construction object
                    gbXMLSerializer.Construction construction = openingType.TogbXML();

                    // If the conversion was successful
                    if (construction != null)
                    {
                        // Add the construction to the list of constructions
                        constructions.Add(construction);
                    }
                }
            }

            // Create a new gbXML object
            gbXMLSerializer.gbXML gbXML = new gbXMLSerializer.gbXML();

            // Set some default properties for the gbXML object
            gbXML.useSIUnitsForResults = "true";
            gbXML.temperatureUnit = temperatureUnitEnum.C;
            gbXML.lengthUnit = lengthUnitEnum.Meters;
            gbXML.areaUnit = areaUnitEnum.SquareMeters;
            gbXML.volumeUnit = volumeUnitEnum.CubicMeters;
            gbXML.version = versionEnum.FiveOneOne;

            // Convert the BuildingModel to a gbXML Campus object and set it on the gbXML object
            gbXML.Campus = buildingModel.TogbXML_Campus(silverSpacing, tolerance_Angle, tolerance_Distance);
            gbXML.Constructions = constructions.ToArray();
            gbXML.DocumentHistory = Core.gbXML.Query.DocumentHistory(buildingModel.Guid);

            return gbXML;
        }

    }
}
