﻿namespace SAM.Core.gbXML
{
    public static partial class Convert
    {
        public static gbXMLSerializer.Location TogbXML(this Location location, Address address = null, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (location == null)
                return null;

            gbXMLSerializer.Location location_gbXML = new gbXMLSerializer.Location();
            location_gbXML.Latitude = Core.Query.Round(location.Latitude, tolerance).ToString();
            location_gbXML.Longitude = Core.Query.Round(location.Longitude, tolerance).ToString();
            location_gbXML.Name = location.Name;
            location_gbXML.CADModelAzimuth = 0;

            string postalCode = address?.PostalCode;
            if (!string.IsNullOrWhiteSpace(postalCode))
                location_gbXML.ZipcodeOrPostalCode = postalCode;
            else
                location_gbXML.ZipcodeOrPostalCode = "000000";

            return location_gbXML;
        }

    }
}
