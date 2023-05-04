using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// This class contains methods to convert SAM materials to gbXML materials
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// This method converts a SAM material to a gbXML material
        /// </summary>
        /// <param name="material">The SAM material to convert</param>
        /// <returns>The corresponding gbXML material</returns>
        public static Material TogbXML(this Core.IMaterial material)
        {
            // If the input material is null, return null
            if (material == null)
            {
                return null;
            }

            // Create a new Material object
            Material result = new();

            // Set the id of the Material object to the id of the input material
            result.id = Core.gbXML.Query.Id(material.Name);

            //// If the input material is a Core.Material object, set the name of the Material object to the DisplayName of the Core.Material object,
            //// otherwise set it to the name of the input material
            //result.Name = material is Core.Material ? ((Core.Material)material).DisplayName : material.Name;
            result.Name = material switch
            {
                Core.Material coreMaterial => coreMaterial.DisplayName,
                _ => material.Name
            };

            // If the name of the Material object is null or whitespace, set it to the name of the input material
            if (string.IsNullOrWhiteSpace(result.Name))
            {
                result.Name = material.Name;
            }

            // If the input material is a Core.Material object, set the Conductivity, SpecificHeat, Density, and Thickness properties of the Material object
            if (material is Core.Material material_Temp)
            {
                //Core.Material material_Temp = (Core.Material)material;

                // Set the Conductivity property of the Material object if ThermalConductivity is not NaN
                if (!double.IsNaN(material_Temp.ThermalConductivity))
                {
                    result.Conductivity = new ();
                    result.Conductivity.value = material_Temp.ThermalConductivity;
                    result.Conductivity.unit = conductivityUnitEnum.WPerMeterK;
                }

                // Set the SpecificHeat property of the Material object if SpecificHeatCapacity is not NaN
                if (!double.IsNaN(material_Temp.SpecificHeatCapacity))
                {
                    result.SpecificHeat = new ();
                    result.SpecificHeat.value = material_Temp.SpecificHeatCapacity;
                    result.SpecificHeat.unit = specificHeatEnum.JPerKgK;
                }

                // Set the Density property of the Material object if Density is not NaN
                if (!double.IsNaN(material_Temp.Density))
                {
                    result.Density = new ();
                    result.Density.value = material_Temp.Density;
                    result.Density.unit = densityUnitEnum.KgPerCubicM;
                }

                // Set the Thickness property of the Material object if the input material has a value for the DefaultThickness parameter
                if (material_Temp.TryGetValue(Core.MaterialParameter.DefaultThickness, out double defaultThickness) && !double.IsNaN(defaultThickness))
                {
                    result.Thickness = new ();
                    result.Thickness.value = defaultThickness;
                    result.Thickness.unit = lengthUnitEnum.Meters;
                }

                // Set the Reflectance and Absorptance properties of the Material object using the Create helper class
                result.Reflectance = Create.Reflectances(material_Temp);
                result.Absorptance = Create.Absorptances(material_Temp);

                // Set the Description property of the Material object to the Description of the input material
                result.Description = material_Temp.Description;
            }

            // Return the Material object
            return result;
        }
    }
}
