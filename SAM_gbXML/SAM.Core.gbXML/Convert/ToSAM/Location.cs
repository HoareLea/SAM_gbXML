namespace SAM.Core.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a gbXML location to a SAM location with the specified tolerance for rounding coordinates.
        /// </summary>
        /// <param name="location">The gbXML location to convert.</param>
        /// <param name="tolerance">The tolerance for rounding coordinates.</param>
        /// <returns>A new SAM location object with the same values as the gbXML location.</returns>
        public static Location ToSAM(this gbXMLSerializer.Location location, double tolerance = Tolerance.MicroDistance)
        {
            if (location == null)
                return null;

            double longitude;
            if (!double.TryParse(location.Longitude, out longitude))
                longitude = 0;

            double latitude;
            if (!double.TryParse(location.Latitude, out latitude))
                latitude = 0;

            Location result = new Location(location.Name, Core.Query.Round(longitude, tolerance), Core.Query.Round(latitude, tolerance), 0);

            return result;
        }

    }
}
