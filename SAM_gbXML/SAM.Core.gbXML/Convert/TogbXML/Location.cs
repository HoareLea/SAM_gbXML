namespace SAM.Core.gbXML
{
    /// <summary>
    /// Define static class containing extension methods for SAM Location
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a SAM Location object to a gbXML Location object.
        /// </summary>
        /// <param name="location">The SAM Location object to convert.</param>
        /// <param name="address">The SAM Address object containing the postal code to include in the gbXML Location object. Defaults to null.</param>
        /// <param name="tolerance">The tolerance used to round the latitude and longitude values of the SAM Location object. Defaults to Tolerance.MicroDistance.</param>
        /// <returns>A gbXML Location object.</returns>
        public static gbXMLSerializer.Location TogbXML(this Location location, Address address = null, double tolerance = Tolerance.MicroDistance)
        {
            if (location == null)
                return null;

            gbXMLSerializer.Location location_gbXML = new gbXMLSerializer.Location
            {
                Latitude = Core.Query.Round(location.Latitude, tolerance).ToString(),
                Longitude = Core.Query.Round(location.Longitude, tolerance).ToString(),
                Name = location.Name,
                CADModelAzimuth = 0
            };

            string postalCode = address?.PostalCode;
            if (!string.IsNullOrWhiteSpace(postalCode))
                location_gbXML.ZipcodeOrPostalCode = postalCode;
            else
                location_gbXML.ZipcodeOrPostalCode = "000000";

            return location_gbXML;
        }

    }
}
