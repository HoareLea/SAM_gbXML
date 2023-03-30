using SAM.Core;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Material ToSAM(this gbXMLSerializer.Material material)
        {
            if (material == null)
            {
                return null;
            }

            double thermalConductivity = material.Conductivity.Value();
            double specificHeatCapacity = material.SpecificHeat.Value();
            double density = material.Density.Value();

            Material result = null;

            double rValue = material.RValue.Value();
            if(double.IsNaN(rValue) || rValue == 0)
            {
                result = new OpaqueMaterial(material.id, null, material.Name, material.Description, thermalConductivity, specificHeatCapacity, density);
            }
            else
            {
                result = new GasMaterial(material.id, null, material.Name, material.Description, thermalConductivity, specificHeatCapacity, density, double.NaN);
                result.SetValue(GasMaterialParameter.HeatTransferCoefficient, 1 / rValue);
            }

            double thickness = material.Thickness.Value();
            if(!double.IsNaN(thickness))
            {
                result.SetValue(Core.MaterialParameter.DefaultThickness, thickness);
            }

            return result;
        }

    }
}
