using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A class that contains methods for creating gbXML objects.
    /// </summary>
    public static partial class Create
    {
        /// <summary>
        /// Gets the emittances of the specified material.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <returns>The array of emittances, or null if the material is null or has no emittances.</returns>
        public static gbXMLSerializer.Emittance[] Emittances(this Material material)
        {
            if (material == null)
            {
                return null;
            }

            // Create a list to hold the emittances
            List<gbXMLSerializer.Emittance> emittances = new List<gbXMLSerializer.Emittance>();

            // Set the surface description enum to both
            gbXMLSerializer.surfaceDescriptionEnum surfaceDescriptionEnum = gbXMLSerializer.surfaceDescriptionEnum.Both;

            // Get the external emissivity of the material, if it exists
            if (material.TryGetValue(TransparentMaterialParameter.ExternalEmissivity, out double externalEmissivity))
            {
                // Create a new emittance object with the external emissivity
                gbXMLSerializer.Emittance emittance = new gbXMLSerializer.Emittance();
                emittance.surfaceType = surfaceDescriptionEnum;
                emittance.type = gbXMLSerializer.emittanceTypeEnum.ExtIR;
                emittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                emittance.value = externalEmissivity;

                // Add the emittance to the list
                emittances.Add(emittance);
            }

            // Get the internal emissivity of the material, if it exists
            if (material.TryGetValue(TransparentMaterialParameter.InternalEmissivity, out double internalEmissivity))
            {
                // Create a new emittance object with the internal emissivity
                gbXMLSerializer.Emittance emittance = new gbXMLSerializer.Emittance();
                emittance.surfaceType = surfaceDescriptionEnum;
                emittance.type = gbXMLSerializer.emittanceTypeEnum.IntIR;
                emittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                emittance.value = internalEmissivity;

                // Add the emittance to the list
                emittances.Add(emittance);
            }

            // If there are no emittances in the list, return null
            if (emittances == null || emittances.Count == 0)
            {
                return null;
            }

            // Otherwise, return the emittances as an array
            return emittances.ToArray();
        }
    }
}
