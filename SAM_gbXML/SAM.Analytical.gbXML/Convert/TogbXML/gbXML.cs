using gbXMLSerializer;
using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Provides conversion of an analytical model to gbXML format.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts an analytical model to gbXML format.
        /// </summary>
        /// <param name="analyticalModel">SAM Analytical model to be converted.</param>
        /// <param name="silverSpacing">Distance used to calculate silver surfaces. Default is macro distance.</param>
        /// <param name="tolerance">Tolerance used for numerical comparisons. Default is micro distance.</param>
        /// <returns>Returns gbXML format of the given analytical model.</returns>
        public static gbXMLSerializer.gbXML TogbXML(this AnalyticalModel analyticalModel, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.MicroDistance)
        {
            // Check if the analytical model is null
            if (analyticalModel == null)
            {
                return null;
            }

            // Create a temporary copy of the analytical model
            AnalyticalModel analyticalModel_Temp = new AnalyticalModel(analyticalModel);

            // Split materials by thickness
            analyticalModel_Temp.SplitMaterialsByThickness(true, false);

            // Initialize lists to store gbXML data
            List<gbXMLSerializer.Construction> constructions_gbXML = new List<gbXMLSerializer.Construction>();
            List<Layer> layers_gbXML = new List<Layer>();
            List<gbXMLSerializer.Material> materials_gbXML = new List<gbXMLSerializer.Material>();
            List<gbXMLSerializer.WindowType> windowTypes_gbXML = new List<gbXMLSerializer.WindowType>();

            // Get constructions and corresponding layers from the analytical model
            List<Construction> constructions = analyticalModel_Temp.AdjacencyCluster?.GetConstructions<IPanel>();
            if (constructions != null)
            {
                foreach (Construction construction in constructions)
                {
                    // Convert construction to gbXML format
                    gbXMLSerializer.Construction construction_gbXML = construction.TogbXML(tolerance);
                    if (construction_gbXML != null)
                    {
                        constructions_gbXML.Add(construction_gbXML);
                    }

                    // Convert construction layer to gbXML format
                    Layer layer_gbXML = construction.TogbXML_Layer();
                    if (layer_gbXML != null)
                    {
                        layers_gbXML.Add(layer_gbXML);
                    }
                }
            }

            // Get materials from the analytical model
            MaterialLibrary materialLibrary = analyticalModel_Temp.MaterialLibrary;
            if (materialLibrary != null)
            {
                foreach (IMaterial material in materialLibrary.GetMaterials())
                {
                    // Convert material to gbXML format
                    gbXMLSerializer.Material material_gbXML = material?.TogbXML();
                    if (material_gbXML != null)
                    {
                        materials_gbXML.Add(material_gbXML);
                    }
                }
            }

            // Get window types from the analytical model
            List<ApertureConstruction> apertureConstructions = analyticalModel_Temp.AdjacencyCluster?.GetApertureConstructions();
            if (apertureConstructions != null)
            {
                foreach (ApertureConstruction apertureConstruction in apertureConstructions)
                {
                    // Convert aperture construction to gbXML window type
                    gbXMLSerializer.WindowType windowType_gbXML = apertureConstruction.TogbXML(materialLibrary);
                    if (windowType_gbXML != null)
                    {
                        windowTypes_gbXML.Add(windowType_gbXML);
                    }
                }
            }

            // Create gbXML object and populate its properties
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
