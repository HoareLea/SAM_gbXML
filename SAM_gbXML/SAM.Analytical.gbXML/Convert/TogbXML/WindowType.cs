using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static gbXMLSerializer.WindowType TogbXML(this ApertureConstruction apertureConstruction, MaterialLibrary materialLibrary)
        {
            if (apertureConstruction == null)
            {
                return null;
            }

            gbXMLSerializer.WindowType result = new gbXMLSerializer.WindowType();
            result.Name = apertureConstruction.Name;
            result.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.WindowType));


            List<gbXMLSerializer.Glaze> glazes = new List<gbXMLSerializer.Glaze>();
            List<gbXMLSerializer.Gap> gaps = new List<gbXMLSerializer.Gap>();
            List<gbXMLSerializer.Frame> frames = new List<gbXMLSerializer.Frame>();

            int index = 1;

            List<ConstructionLayer> constructionLayers_Pane = apertureConstruction.PaneConstructionLayers;
            if(constructionLayers_Pane != null)
            {
                foreach(ConstructionLayer constructionLayer in constructionLayers_Pane)
                {
                    IMaterial material = constructionLayer?.Material(materialLibrary);
                    if(material is GasMaterial)
                    {
                        GasMaterial gasMaterial = (GasMaterial)material;

                        gbXMLSerializer.Gap gap = new gbXMLSerializer.Gap();
                        gap.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Gap)) + "_" + index.ToString();

                        gap.Name = gasMaterial.Name;
                        gap.Description = gasMaterial.Description;
                        
                        if(!double.IsNaN(constructionLayer.Thickness))
                        {
                            gap.Thickness = new gbXMLSerializer.Thickness();
                            gap.Thickness.value = constructionLayer.Thickness;
                            gap.Thickness.unit = gbXMLSerializer.lengthUnitEnum.Meters;
                        }

                        if (!double.IsNaN(gasMaterial.ThermalConductivity))
                        {
                            gap.Conductivity = new gbXMLSerializer.Conductivity();
                            gap.Conductivity.value = gasMaterial.ThermalConductivity;
                            gap.Conductivity.unit = gbXMLSerializer.conductivityUnitEnum.WPerMeterK;
                        }

                        gaps.Add(gap);
                    }
                    else if(material is TransparentMaterial)
                    {
                        TransparentMaterial transparentMaterial = (TransparentMaterial)material;

                        gbXMLSerializer.Glaze glaze = new gbXMLSerializer.Glaze();
                        glaze.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Glaze)) + "_" + index.ToString();

                        glaze.Name = transparentMaterial.Name;
                        glaze.Description = transparentMaterial.Description;

                        if (!double.IsNaN(constructionLayer.Thickness))
                        {
                            glaze.Thickness = new gbXMLSerializer.Thickness();
                            glaze.Thickness.value = constructionLayer.Thickness;
                            glaze.Thickness.unit = gbXMLSerializer.lengthUnitEnum.Meters;
                        }

                        if (!double.IsNaN(transparentMaterial.ThermalConductivity))
                        {
                            glaze.Conductivity = new gbXMLSerializer.Conductivity();
                            glaze.Conductivity.value = transparentMaterial.ThermalConductivity;
                            glaze.Conductivity.unit = gbXMLSerializer.conductivityUnitEnum.WPerMeterK;
                        }

                        glazes.Add(glaze);
                    }
                    else if (material is OpaqueMaterial)
                    {
                        OpaqueMaterial opaqueMaterial = (OpaqueMaterial)material;

                        gbXMLSerializer.Frame frame = new gbXMLSerializer.Frame();
                        frame.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Frame)) + "_" + index.ToString();

                        frame.Name = opaqueMaterial.Name;
                        frame.Description = opaqueMaterial.Description;

                        frame.type = gbXMLSerializer.frameTypeEnum.Insulated;

                        if(!double.IsNaN(constructionLayer.Thickness))
                        {
                            frame.Width = new gbXMLSerializer.Width();
                            frame.Width.value = constructionLayer.Thickness;
                            frame.Width.unit = gbXMLSerializer.lengthUnitEnum.Meters;
                        }

                        frames.Add(frame);
                    }

                    index++;
                }
            }

            List<ConstructionLayer> constructionLayer_Frame = apertureConstruction.FrameConstructionLayers;
            if(constructionLayer_Frame != null)
            {
                List<gbXMLSerializer.Frame> frames_Temp = new List<gbXMLSerializer.Frame>();
                foreach (ConstructionLayer constructionLayer in constructionLayer_Frame)
                {
                    IMaterial material = constructionLayer?.Material(materialLibrary);

                    if (material is OpaqueMaterial)
                    {
                        OpaqueMaterial opaqueMaterial = (OpaqueMaterial)material;

                        gbXMLSerializer.Frame frame = new gbXMLSerializer.Frame();
                        frame.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Frame)) + "_" + index.ToString();

                        frame.Name = opaqueMaterial.Name;
                        frame.Description = opaqueMaterial.Description;

                        frame.type = gbXMLSerializer.frameTypeEnum.Insulated;

                        if(!double.IsNaN(constructionLayer.Thickness))
                        {
                            frame.Width = new gbXMLSerializer.Width();
                            frame.Width.value = constructionLayer.Thickness;
                            frame.Width.unit = gbXMLSerializer.lengthUnitEnum.Meters;
                        }

                        frames_Temp.Add(frame);
                    }

                    index++;
                }

                if(frames_Temp != null && frames_Temp.Count != 0)
                {
                    frames = frames_Temp;
                }
            }

            result.Glaze = glazes.ToArray();
            result.Gap = gaps.ToArray();
            result.Frame = frames.ToArray();

            return result;
        }

    }
}
