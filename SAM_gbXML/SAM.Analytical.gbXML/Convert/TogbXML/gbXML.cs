using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static gbXMLSerializer.gbXML TogbXML(this AnalyticalModel analyticalModel, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (analyticalModel == null)
                return null;

            gbXMLSerializer.gbXML gbXML = new gbXMLSerializer.gbXML();
            gbXML.useSIUnitsForResults = "true";
            gbXML.temperatureUnit = temperatureUnitEnum.C;
            gbXML.lengthUnit = lengthUnitEnum.Meters;
            gbXML.areaUnit = areaUnitEnum.SquareMeters;
            gbXML.volumeUnit = volumeUnitEnum.CubicMeters;
            gbXML.version = versionEnum.FiveOneOne;
            gbXML.Campus = analyticalModel.TogbXML_Campus(tolerance);

            return gbXML;
        }

    }
}
