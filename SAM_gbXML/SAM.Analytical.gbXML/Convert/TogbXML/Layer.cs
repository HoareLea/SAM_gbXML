using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Static class containing extension methods for converting analytical model entities to gbXML entities.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts the construction to a gbXML layer.
        /// </summary>
        /// <param name="construction">The construction to convert.</param>
        /// <returns>The converted gbXML layer.</returns>
        public static Layer TogbXML_Layer(this Construction construction)
        {
            // Get the construction layers
            List<ConstructionLayer> constructionLayers = construction?.ConstructionLayers;
            if (constructionLayers == null)
            {
                return null;
            }

            // Create a new gbXML layer and set its ID
            Layer result = new Layer();
            result.id = Core.gbXML.Query.Id(construction, typeof(Layer));

            // Get the material IDs for each construction layer and add them to the layer
            List<MaterialId> materialIds = new List<MaterialId>();
            foreach (ConstructionLayer constructionLayer in constructionLayers)
            {
                if (string.IsNullOrWhiteSpace(constructionLayer?.Name))
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
