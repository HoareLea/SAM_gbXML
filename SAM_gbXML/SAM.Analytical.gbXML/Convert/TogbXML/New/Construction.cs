namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Provides extension methods to convert objects of type HostPartitionType and OpeningType to objects of type gbXMLSerializer.Construction.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a HostPartitionType object to a gbXMLSerializer.Construction object.
        /// </summary>
        /// <param name="hostPartitionType">The HostPartitionType object to be converted.</param>
        /// <param name="tolerance">A double value representing the tolerance to use during the conversion process. Default is Core.Tolerance.MicroDistance.</param>
        /// <returns>A new gbXMLSerializer.Construction object containing the converted data, or null if the hostPartitionType parameter is null.</returns>
        public static gbXMLSerializer.Construction TogbXML(this HostPartitionType hostPartitionType, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (hostPartitionType == null)
                return null;

            gbXMLSerializer.Construction result = new gbXMLSerializer.Construction()
            {
                // Sets the id property of the new gbXMLSerializer.Construction object to the value returned by
                // the Core.gbXML.Query.Id method, passing the hostPartitionType object and typeof(gbXMLSerializer.Construction)
                // as arguments.
                id = Core.gbXML.Query.Id(hostPartitionType, typeof(gbXMLSerializer.Construction)),
                // Sets the Name property of the new gbXMLSerializer.Construction object to the value of the Name
                // property of the hostPartitionType object.
                Name = hostPartitionType.Name
            };

            return result;
        }

        /// <summary>
        /// Converts an OpeningType object to a gbXMLSerializer.Construction object.
        /// </summary>
        /// <param name="openingType">The OpeningType object to be converted.</param>
        /// <param name="tolerance">A double value representing the tolerance to use during the conversion process. Default is Core.Tolerance.MicroDistance.</param>
        /// <returns>A new gbXMLSerializer.Construction object containing the converted data, or null if the openingType parameter is null.</returns>
        public static gbXMLSerializer.Construction TogbXML(this OpeningType openingType, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (openingType == null)
                return null;

            gbXMLSerializer.Construction result = new gbXMLSerializer.Construction()
            {
                // Sets the id property of the new gbXMLSerializer.Construction object to the value returned by
                // the Core.gbXML.Query.Id method, passing the openingType object and typeof(gbXMLSerializer.Construction)
                // as arguments.
                id = Core.gbXML.Query.Id(openingType, typeof(gbXMLSerializer.Construction)),
                // Sets the Name property of the new gbXMLSerializer.Construction object to the value of the Name
                // property of the openingType object.
                Name = openingType.Name
            };

            return result;
        }
    }
}

