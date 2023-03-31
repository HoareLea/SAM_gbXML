using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Material TogbXML(this Core.IMaterial material)
        {
            if(material == null)
            {
                return null;
            }

            Material result = new Material();
            result.id = material.Name;
            result.Name = material is Core.Material ? ((Core.Material)material).DisplayName : material.Name;
            if (string.IsNullOrWhiteSpace(result.Name))
            {
                result.Name = material.Name;
            }

            if(material is Core.Material)
            {
                Core.Material material_Temp = (Core.Material)material;

                result.Conductivity = new Conductivity();
                result.Conductivity.value = material_Temp.ThermalConductivity;
                result.Conductivity.unit = conductivityUnitEnum.WPerMeterK;

                result.SpecificHeat = new SpecificHeat();
                result.SpecificHeat.value = material_Temp.SpecificHeatCapacity;
                result.SpecificHeat.unit = specificHeatEnum.JPerKgK;

                result.Density = new Density();
                result.Density.value = material_Temp.Density;
                result.Density.unit = densityUnitEnum.KgPerCubicM;

                result.Description = material_Temp.Description;
            }


            return result;
        }

    }
}
