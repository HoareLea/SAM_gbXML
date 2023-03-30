namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static gbXMLSerializer.Layer Layer(this gbXMLSerializer.gbXML gbXML, string id)
        {
            if(gbXML == null || string.IsNullOrEmpty(id))
            {
                return null;
            }

            gbXMLSerializer.Layer[] layers = gbXML.Layers;
            if(layers == null)
            {
                return null;
            }

            foreach(gbXMLSerializer.Layer layer in layers)
            {
                if(id.Equals(layer?.id))
                {
                    return layer;
                }
            }

            return null;
        }
    }
}