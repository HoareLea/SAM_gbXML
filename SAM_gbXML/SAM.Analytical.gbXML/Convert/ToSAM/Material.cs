using SAM.Core;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Provides conversion methods for gbXML objects to SAM Analytical objects.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a gbXML Material object to a SAM Material object.
        /// </summary>
        /// <param name="material">The gbXML Material object to convert.</param>
        /// <returns>The resulting SAM Material object.</returns>
        public static Material ToSAM(this gbXMLSerializer.Material material)
        {
            if (material == null)
            {
                return null;
            }

            // Extract necessary properties from gbXML Material
            double thermalConductivity = material.Conductivity.Value();
            double specificHeatCapacity = material.SpecificHeat.Value();
            double density = material.Density.Value();

            Material result = null;

            double rValue = material.RValue.Value();
            if (double.IsNaN(rValue) || rValue == 0)
            {
                // If the material is opaque, create an OpaqueMaterial object
                result = new OpaqueMaterial(material.id, null, material.Name, material.Description, thermalConductivity, specificHeatCapacity, density);
            }
            else
            {
                // If the material is not opaque, create a GasMaterial object
                result = new GasMaterial(material.id, null, material.Name, material.Description, thermalConductivity, specificHeatCapacity, density, double.NaN);
                result.SetValue(GasMaterialParameter.HeatTransferCoefficient, 1 / rValue);
            }

            double thickness = material.Thickness.Value();
            if (!double.IsNaN(thickness))
            {
                // Set the material's default thickness, if available
                result.SetValue(Core.MaterialParameter.DefaultThickness, thickness);
            }

            return result;
        }

    }
}
