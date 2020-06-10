using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Location TogbXML(this Core.Location location, Core.Address address = null, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (location == null)
                return null;

            Location location_gbXML = new Location();
            location_gbXML.Latitude = Core.Query.Round(location.Latitude, tolerance).ToString();
            location_gbXML.Longitude = Core.Query.Round(location.Longitude, tolerance).ToString();
            location_gbXML.Name = location.Name;
            location_gbXML.CADModelAzimuth = 0;

            string postalCode = address?.PostalCode;
            if (!string.IsNullOrWhiteSpace(postalCode))
                location_gbXML.ZipcodeOrPostalCode = postalCode;

            return location_gbXML;
        }

    }
}
