using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Layer TogbXML_Layer(this Construction construction)
        {
            List<ConstructionLayer> constructionLayers = construction?.ConstructionLayers;
            if(constructionLayers == null)
            {
                return null;
            }

            Layer result = new Layer();
            result.id = Core.gbXML.Query.Id(construction, typeof(Layer));

            List<MaterialId> materialIds = new List<MaterialId>();
            foreach(ConstructionLayer constructionLayer in constructionLayers)
            {
                if(string.IsNullOrWhiteSpace(constructionLayer?.Name))
                {
                    continue;
                }

                materialIds.Add(new MaterialId() { materialIdRef = Core.gbXML.Query.Id(constructionLayer.Name), percentOfLayer = 100 });
            }

            result.MaterialId = materialIds.ToArray();

            return result;

        }

    }
}
