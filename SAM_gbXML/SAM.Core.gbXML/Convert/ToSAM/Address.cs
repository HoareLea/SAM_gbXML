namespace SAM.Core.gbXML
{
    public static partial class Convert
    {
        public static Address ToSAM_Address(this gbXMLSerializer.Location location)
        {
            if (location == null)
                return null;

            Address result = new Address(string.Empty, string.Empty, location.ZipcodeOrPostalCode, CountryCode.Undefined);

            return result;
        }

    }
}
