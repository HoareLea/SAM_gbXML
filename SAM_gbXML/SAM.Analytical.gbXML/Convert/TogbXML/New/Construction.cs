namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static gbXMLSerializer.Construction TogbXML(this HostPartitionType hostPartitionType, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (hostPartitionType == null)
                return null;

            gbXMLSerializer.Construction result = new gbXMLSerializer.Construction()
            {
                id = Core.gbXML.Query.Id(hostPartitionType, typeof(gbXMLSerializer.Construction)),
                Name = hostPartitionType.Name
            };

            return result;
        }

        public static gbXMLSerializer.Construction TogbXML(this OpeningType openingType, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (openingType == null)
                return null;

            gbXMLSerializer.Construction result = new gbXMLSerializer.Construction()
            {
                id = Core.gbXML.Query.Id(openingType, typeof(gbXMLSerializer.Construction)),
                Name = openingType.Name
            };


            return result;
        }

    }
}
