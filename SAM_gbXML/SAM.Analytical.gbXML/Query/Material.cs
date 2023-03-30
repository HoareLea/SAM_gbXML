namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static gbXMLSerializer.Material Material(this gbXMLSerializer.gbXML gbXML, string id)
        {
            if(gbXML == null || string.IsNullOrEmpty(id))
            {
                return null;
            }

            gbXMLSerializer.Material[] materials = gbXML.Materials;
            if(materials == null)
            {
                return null;
            }

            foreach(gbXMLSerializer.Material material in materials)
            {
                if(id.Equals(material?.id))
                {
                    return material;
                }
            }

            return null;
        }
    }
}