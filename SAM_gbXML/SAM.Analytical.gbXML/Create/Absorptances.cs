using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Helper class to create gbXML Absorptance objects from Material objects
    /// </summary>
    public static partial class Create
    {
        /// <summary>
        /// Creates an array of gbXML Absorptance objects from a Material object
        /// </summary>
        /// <param name="material">The Material object to create Absorptance objects from</param>
        /// <returns>An array of gbXML Absorptance objects, or null if no Absorptance objects can be created</returns>
        public static gbXMLSerializer.Absorptance[] Absorptances(this Material material)
        {
            if (material == null)
            {
                return null;
            }

            List<gbXMLSerializer.Absorptance> absorptances = new List<gbXMLSerializer.Absorptance>();

            // Create an ExtIR Absorptance object from the ExternalEmissivity property of the Material object
            if (material.TryGetValue(OpaqueMaterialParameter.ExternalEmissivity, out double externalEmissivity))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.ExtIR;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - externalEmissivity;

                absorptances.Add(absorptance);
            }

            // Create an ExtVisible Absorptance object from the ExternalLightReflectance property of the Material object
            if (material.TryGetValue(OpaqueMaterialParameter.ExternalLightReflectance, out double externalLightReflectance))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.ExtVisible;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - externalLightReflectance;

                absorptances.Add(absorptance);
            }

            // Create an ExtSolar Absorptance object from the ExternalSolarReflectance property of the Material object
            if (material.TryGetValue(OpaqueMaterialParameter.ExternalSolarReflectance, out double externalSolarReflectance))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.ExtSolar;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - externalSolarReflectance;

                absorptances.Add(absorptance);
            }

            // Create an IntIR Absorptance object from the InternalEmissivity property of the Material object
            if (material.TryGetValue(OpaqueMaterialParameter.InternalEmissivity, out double internalEmissivity))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.IntIR;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - internalEmissivity;

                absorptances.Add(absorptance);
            }

            // Create an IntVisible Absorptance object from the InternalLightReflectance property of the Material object
            if (material.TryGetValue(OpaqueMaterialParameter.InternalLightReflectance, out double internalLightReflectance))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.IntVisible;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - internalLightReflectance;

                absorptances.Add(absorptance);
            }

            // Create an IntSolar Absorptance object from the InternalSolarReflectance property of the Material object
            if (material.TryGetValue(OpaqueMaterialParameter.InternalSolarReflectance, out double internalSolarReflectance))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.IntSolar;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - internalSolarReflectance;

                absorptances.Add(absorptance);
            }

            if (absorptances == null || absorptances.Count == 0)
            {
                return null;
            }

            return absorptances.ToArray();
        }
    }
}