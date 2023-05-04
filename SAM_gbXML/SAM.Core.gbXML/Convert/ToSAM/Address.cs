namespace SAM.Core.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a gbXML location object to a SAM address object.
        /// </summary>
        /// <param name="location">The gbXML location object to convert.</param>
        /// <returns>A SAM address object.</returns>
        public static Address ToSAM_Address(this gbXMLSerializer.Location location)
        {
            if (location == null)
                return null;

            Address result = new Address(string.Empty, string.Empty, location.ZipcodeOrPostalCode, CountryCode.Undefined);

            return result;
        }
    }
}
