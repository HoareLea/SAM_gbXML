using gbXMLSerializer;
using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Construction ToSAM(this gbXMLSerializer.Construction construction, gbXMLSerializer.gbXML gbXML = null)
        {
            if (construction == null)
            {
                return null;
            }

            List<ConstructionLayer> constructionLayers = null;

            LayerId[] layerIds = construction.LayerId;
            if (layerIds != null && gbXML != null)
            {
                constructionLayers = new List<ConstructionLayer>();
                foreach (LayerId layerId in layerIds)
                {
                    string id_Layer = layerId?.layerIdRef;
                    if(string.IsNullOrWhiteSpace(id_Layer))
                    {
                        continue;
                    }

                    Layer layer = gbXML?.Layer(id_Layer);
                    if(layer == null)
                    {
                        continue;
                    }

                    MaterialId[] materialLayers = layer.MaterialId;
                    if(materialLayers == null)
                    {
                        continue;
                    }

                    foreach(MaterialId materialId in materialLayers)
                    {
                        if(materialId == null)
                        {
                            continue;
                        }

                        string id_Material = materialId.materialIdRef;
                        gbXMLSerializer.Material material_gbXML = gbXML.Material(id_Material);
                        if(material_gbXML != null)
                        {
                            double thickness = material_gbXML.Thickness.Value();

                            constructionLayers.Add(new ConstructionLayer(id_Material, thickness));
                        }
                    }

                    
                }
            }

            Construction result = new Construction(construction.Name, constructionLayers);
            result.SetValue(Analytical.ConstructionParameter.Description, construction.Description);
            result.SetValue(ConstructionParameter.Id, construction.id);

            return result;
        }

    }
}
