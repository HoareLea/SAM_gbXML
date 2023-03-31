using gbXMLSerializer;
using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static gbXMLSerializer.gbXML TogbXML(this AnalyticalModel analyticalModel, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.MicroDistance)
        {
            if (analyticalModel == null)
            {
                return null;
            }

            AnalyticalModel analyticalModel_Temp = new AnalyticalModel(analyticalModel);

            analyticalModel_Temp.SplitMaterialsByThickness(true, false);

            List<gbXMLSerializer.Construction> constructions_gbXML = new List<gbXMLSerializer.Construction>();
            List<Layer> layers_gbXML = new List<Layer>();

            List<Construction> constructions = analyticalModel_Temp.AdjacencyCluster?.GetConstructions();
            if(constructions != null)
            {
                foreach(Construction construction in constructions)
                {
                    gbXMLSerializer.Construction construction_gbXML = construction.TogbXML(tolerance);
                    if(construction_gbXML != null)
                    {
                        constructions_gbXML.Add(construction_gbXML);
                    }

                    Layer layer_gbXML = construction.TogbXML_Layer();
                    if(layer_gbXML != null)
                    {
                        layers_gbXML.Add(layer_gbXML);
                    }
                }
            }

            List<gbXMLSerializer.Material> materials_gbXML = new List<gbXMLSerializer.Material>();
            MaterialLibrary materialLibrary = analyticalModel_Temp.MaterialLibrary;
            if(materialLibrary != null)
            {
                foreach(IMaterial material in materialLibrary.GetMaterials())
                {
                    gbXMLSerializer.Material material_gbXML = material?.TogbXML();
                    if(material_gbXML != null)
                    {
                        materials_gbXML.Add(material_gbXML);
                    }
                }
            }

            List<gbXMLSerializer.WindowType> windowTypes_gbXML = new List<gbXMLSerializer.WindowType>();
            List<ApertureConstruction> apertureConstructions = analyticalModel_Temp.AdjacencyCluster?.GetApertureConstructions();
            if(apertureConstructions != null)
            {
                foreach (ApertureConstruction apertureConstruction in apertureConstructions)
                {
                    gbXMLSerializer.WindowType windowType_gbXML = apertureConstruction.TogbXML(materialLibrary);
                    if (windowType_gbXML != null)
                    {
                        windowTypes_gbXML.Add(windowType_gbXML);
                    }
                }
            }

            gbXMLSerializer.gbXML gbXML = new gbXMLSerializer.gbXML();
            gbXML.useSIUnitsForResults = "true";
            gbXML.temperatureUnit = temperatureUnitEnum.C;
            gbXML.lengthUnit = lengthUnitEnum.Meters;
            gbXML.areaUnit = areaUnitEnum.SquareMeters;
            gbXML.volumeUnit = volumeUnitEnum.CubicMeters;
            gbXML.version = versionEnum.FiveOneOne;
            gbXML.Campus = analyticalModel_Temp.TogbXML_Campus(silverSpacing, tolerance);
            gbXML.Constructions = constructions_gbXML.ToArray();
            gbXML.Materials = materials_gbXML.ToArray();
            gbXML.Layers = layers_gbXML.ToArray();
            gbXML.DocumentHistory = Core.gbXML.Query.DocumentHistory(analyticalModel_Temp.Guid);
            gbXML.WindowTypes = windowTypes_gbXML.ToArray();

            return gbXML;
        }

    }
}
