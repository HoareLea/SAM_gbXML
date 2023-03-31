namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static gbXMLSerializer.Construction TogbXML(this Construction construction, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (construction == null)
                return null;

            gbXMLSerializer.Construction construction_gbXML = new gbXMLSerializer.Construction();
            construction_gbXML.id = Core.gbXML.Query.Id(construction, typeof(gbXMLSerializer.Construction));
            construction_gbXML.Name = construction.Name;
            construction_gbXML.LayerId = new gbXMLSerializer.LayerId[] { new gbXMLSerializer.LayerId() { layerIdRef = Core.gbXML.Query.Id(construction, typeof(gbXMLSerializer.Layer)) } };

            return construction_gbXML;
        }

        public static gbXMLSerializer.Construction TogbXML(this ApertureConstruction apertureConstruction, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (apertureConstruction == null)
                return null;

            gbXMLSerializer.Construction construction_gbXML = new gbXMLSerializer.Construction();
            construction_gbXML.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Construction));
            construction_gbXML.Name = apertureConstruction.Name;

            return construction_gbXML;
        }

    }
}
