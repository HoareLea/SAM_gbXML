using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Convert
    {
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
