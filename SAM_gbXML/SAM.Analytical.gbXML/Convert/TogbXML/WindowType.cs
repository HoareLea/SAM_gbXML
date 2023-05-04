using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts an ApertureConstruction object to a gbXMLSerializer.WindowType object
        /// </summary>
        /// <param name="apertureConstruction">The ApertureConstruction object to be converted</param>
        /// <param name="materialLibrary">The MaterialLibrary to be used for the conversion</param>
        /// <returns>A gbXMLSerializer.WindowType object</returns>
        public static gbXMLSerializer.WindowType TogbXML(this ApertureConstruction apertureConstruction, MaterialLibrary materialLibrary)
        {
            if (apertureConstruction == null)
            {
                return null;
            }

            gbXMLSerializer.WindowType result = new();
            {

                result.Name = apertureConstruction.Name;
                result.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.WindowType));
                result.Transmittance = apertureConstruction.Transmittances();
            }

            List<gbXMLSerializer.Glaze> glazes = new ();
            List<gbXMLSerializer.Gap> gaps = new ();
            List<gbXMLSerializer.Frame> frames = new ();

            int index = 1;

            List<ConstructionLayer> constructionLayers_Pane = apertureConstruction.PaneConstructionLayers;
            if(constructionLayers_Pane != null)
            {
                foreach(ConstructionLayer constructionLayer in constructionLayers_Pane)
                {
                    IMaterial material = constructionLayer?.Material(materialLibrary);
                    if (material is GasMaterial gasMaterial)
                    {
                        //GasMaterial gasMaterial = (GasMaterial)material;

                        //gbXMLSerializer.Gap gap = new gbXMLSerializer.Gap();
                        //gap.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Gap)) + "_" + index.ToString();

                        //gap.Name = gasMaterial.Name;
                        //gap.Description = gasMaterial.Description;

                        gbXMLSerializer.Gap gap = new()
                        {
                            id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Gap)) + "_" + index.ToString(),
                            Name = gasMaterial.Name,
                            Description = gasMaterial.Description
                        };

                        if (!double.IsNaN(constructionLayer.Thickness))
                        {
                            gap.Thickness = new gbXMLSerializer.Thickness
                            {
                                value = constructionLayer.Thickness,
                                unit = gbXMLSerializer.lengthUnitEnum.Meters
                            };
                        }

                        if (!double.IsNaN(gasMaterial.ThermalConductivity))
                        {
                            gap.Conductivity = new ();
                            gap.Conductivity.value = gasMaterial.ThermalConductivity;
                            gap.Conductivity.unit = gbXMLSerializer.conductivityUnitEnum.WPerMeterK;
                        }

                        if (!double.IsNaN(gasMaterial.Density))
                        {
                            gap.Density = new ();
                            gap.Density.value = gasMaterial.Density;
                            gap.Density.unit = gbXMLSerializer.densityUnitEnum.KgPerCubicM;
                        }

                        gaps.Add(gap);
                    }
                    if (material is TransparentMaterial transparentMaterial)
                    {
                        //TransparentMaterial transparentMaterial = (TransparentMaterial)material;

                        gbXMLSerializer.Glaze glaze = new ();
                        glaze.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Glaze)) + "_" + index.ToString();

                        glaze.Name = transparentMaterial.Name;
                        glaze.Description = transparentMaterial.Description;

                        if (!double.IsNaN(constructionLayer.Thickness))
                        {
                            glaze.Thickness = new ();
                            glaze.Thickness.value = constructionLayer.Thickness;
                            glaze.Thickness.unit = gbXMLSerializer.lengthUnitEnum.Meters;
                        }

                        if (!double.IsNaN(transparentMaterial.ThermalConductivity))
                        {
                            glaze.Conductivity = new ();
                            glaze.Conductivity.value = transparentMaterial.ThermalConductivity;
                            glaze.Conductivity.unit = gbXMLSerializer.conductivityUnitEnum.WPerMeterK;
                        }
                        glaze.Reflectance = Create.Reflectances(transparentMaterial);

                        glaze.Transmittance = Create.Transmittances(transparentMaterial);

                        glaze.Emittance = Create.Emittances(transparentMaterial);

                        glazes.Add(glaze);
                    }
                    else if (material is OpaqueMaterial opaqueMaterial)
                    {
                        //OpaqueMaterial opaqueMaterial = (OpaqueMaterial)material;

                        gbXMLSerializer.Frame frame = new ();
                        frame.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Frame)) + "_" + index.ToString();

                        frame.Name = opaqueMaterial.Name;
                        frame.Description = opaqueMaterial.Description;

                        frame.type = gbXMLSerializer.frameTypeEnum.Insulated;

                        if (!double.IsNaN(constructionLayer.Thickness))
                        {
                            frame.Width = new ();
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
                List<gbXMLSerializer.Frame> frames_Temp = new ();
                foreach (ConstructionLayer constructionLayer in constructionLayer_Frame)
                {
                    IMaterial material = constructionLayer?.Material(materialLibrary);

                    if (material is OpaqueMaterial opaqueMaterial)
                    {
                        //OpaqueMaterial opaqueMaterial = (OpaqueMaterial)material;

                        gbXMLSerializer.Frame frame = new ();
                        frame.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Frame)) + "_" + index.ToString();

                        frame.Name = opaqueMaterial.Name;
                        frame.Description = opaqueMaterial.Description;

                        frame.type = gbXMLSerializer.frameTypeEnum.Insulated;

                        if(!double.IsNaN(constructionLayer.Thickness))
                        {
                            frame.Width = new ();
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
