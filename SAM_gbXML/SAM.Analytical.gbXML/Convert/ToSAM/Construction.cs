using gbXMLSerializer; 
using SAM.Core; 
using System.Collections.Generic; 

// Define the namespace and class for the conversion functions.
namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Class containing conversion functions from gbXML objects to SAM objects.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a gbXML construction to a SAM construction.
        /// </summary>
        /// <param name="construction">The gbXML construction to convert.</param>
        /// <param name="gbXML">The gbXML file that the construction is a part of.</param>
        /// <returns>The SAM construction that corresponds to the gbXML construction.</returns>
        public static Construction ToSAM(this gbXMLSerializer.Construction construction, gbXMLSerializer.gbXML gbXML = null)
        {
            // If the gbXML construction is null, return null.
            if (construction == null)
            {
                return null;
            }

            // Declare a list of construction layers.
            List<ConstructionLayer> constructionLayers = null;

            // Get the layer IDs for the construction.
            LayerId[] layerIds = construction.LayerId;
            // If there are layer IDs and a gbXML file is provided.
            if (layerIds != null && gbXML != null)
            {
                // Create a new list for the construction layers.
                constructionLayers = new List<ConstructionLayer>();
                // Loop through the layer IDs.
                foreach (LayerId layerId in layerIds)
                {
                    // Get the ID of the current layer.
                    string id_Layer = layerId?.layerIdRef;
                    // If the ID is empty, skip this layer.
                    if (string.IsNullOrWhiteSpace(id_Layer))
                    {
                        continue;
                    }

                    // Get the layer from the gbXML file.
                    Layer layer = gbXML?.Layer(id_Layer);
                    // If the layer is null, skip this layer.
                    if (layer == null)
                    {
                        continue;
                    }

                    // Get the material IDs for the layer.
                    MaterialId[] materialLayers = layer.MaterialId;
                    // If there are no material IDs, skip this layer.
                    if (materialLayers == null)
                    {
                        continue;
                    }

                    // Loop through the material IDs.
                    foreach (MaterialId materialId in materialLayers)
                    {
                        // If the material ID is null, skip it.
                        if (materialId == null)
                        {
                            continue;
                        }

                        // Get the ID of the current material.
                        string id_Material = materialId.materialIdRef;
                        // Get the material from the gbXML file.
                        gbXMLSerializer.Material material_gbXML = gbXML.Material(id_Material);
                        // If the material is null, skip it.
                        if (material_gbXML != null)
                        {
                            // Get the thickness of the material.
                            double thickness = material_gbXML.Thickness.Value();
                            // Add the material to the list of construction layers.
                            constructionLayers.Add(new ConstructionLayer(id_Material, thickness));
                        }
                    }
                }
            }

            // Create a new SAM construction with the name and layers.
            Construction result = new Construction(construction.Name, constructionLayers);
            // Set the description of the SAM construction.
            result.SetValue(Analytical.ConstructionParameter.Description, construction.Description);
            // Set the ID of the SAM construction.
            result.SetValue(ConstructionParameter.Id, construction.id);

            // Return the SAM construction.
            return result;
        }
    }
}
