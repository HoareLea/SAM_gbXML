using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Create
    {
        /// <summary>
        /// This method returns an array of gbXMLSerializer.Transmittance objects, representing the transmittance values of the given material.
        /// </summary>
        /// <param name="material">The material for which to calculate transmittance values.</param>
        /// <returns>An array of gbXMLSerializer.Transmittance objects representing the transmittance values of the given material.</returns>
        public static gbXMLSerializer.Transmittance[] Transmittances(this Material material)
        {
            if (material == null)
            {
                return null;
            }

            List<gbXMLSerializer.Transmittance> transmittances = new List<gbXMLSerializer.Transmittance>();

            gbXMLSerializer.surfaceDescriptionEnum surfaceDescriptionEnum = gbXMLSerializer.surfaceDescriptionEnum.Both;

            // Check if the material has a light transmittance value and add it to the transmittances list
            if (material.TryGetValue(TransparentMaterialParameter.LightTransmittance, out double lightTransmittance))
            {
                gbXMLSerializer.Transmittance transmittance = new gbXMLSerializer.Transmittance();
                transmittance.surfaceType = surfaceDescriptionEnum;
                transmittance.type = gbXMLSerializer.radiationWavelengthTypeEnum.Visible;
                transmittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                transmittance.value = lightTransmittance;

                transmittances.Add(transmittance);
            }

            // Check if the material has a solar transmittance value and add it to the transmittances list
            if (material.TryGetValue(TransparentMaterialParameter.SolarTransmittance, out double solarTransmittance))
            {
                gbXMLSerializer.Transmittance transmittance = new gbXMLSerializer.Transmittance();
                transmittance.surfaceType = surfaceDescriptionEnum;
                transmittance.type = gbXMLSerializer.radiationWavelengthTypeEnum.Solar;
                transmittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                transmittance.value = solarTransmittance;

                transmittances.Add(transmittance);
            }

            // If there are no transmittance values in the list, return null
            if (transmittances == null || transmittances.Count == 0)
            {
                return null;
            }

            return transmittances.ToArray();
        }
        /// <summary>
        /// This method returns an array of gbXMLSerializer.Transmittance objects, representing the transmittance values of the given aperture construction.
        /// </summary>
        /// <param name="apertureConstruction">The aperture construction for which to calculate transmittance values.</param>
        /// <returns>An array of gbXMLSerializer.Transmittance objects representing the transmittance values of the given aperture construction.</returns>
        public static gbXMLSerializer.Transmittance[] Transmittances(this ApertureConstruction apertureConstruction)
        {
            if (apertureConstruction == null)
            {
                return null;
            }

            List<gbXMLSerializer.Transmittance> transmittances = new List<gbXMLSerializer.Transmittance>();

            gbXMLSerializer.surfaceDescriptionEnum surfaceDescriptionEnum = gbXMLSerializer.surfaceDescriptionEnum.Both;

            // Check if the aperture construction has a light transmittance value and add it to the transmittances list
            if (apertureConstruction.TryGetValue(ApertureConstructionParameter.LightTransmittance, out double lightTransmittance))
            {
                gbXMLSerializer.Transmittance transmittance = new gbXMLSerializer.Transmittance();
                transmittance.surfaceType = surfaceDescriptionEnum;
                transmittance.type = gbXMLSerializer.radiationWavelengthTypeEnum.Visible;
                transmittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                transmittance.value = lightTransmittance;

                transmittances.Add(transmittance);
            }

            // Check if the aperture construction has a total solarEnergy transmittance value and add it to the transmittances list
            if (apertureConstruction.TryGetValue(ApertureConstructionParameter.TotalSolarEnergyTransmittance, out double totalSolarEnergyTransmittance))
            {
                gbXMLSerializer.Transmittance transmittance = new gbXMLSerializer.Transmittance();
                transmittance.surfaceType = surfaceDescriptionEnum;
                transmittance.type = gbXMLSerializer.radiationWavelengthTypeEnum.Solar;
                transmittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                transmittance.value = totalSolarEnergyTransmittance;

                transmittances.Add(transmittance);
            }

            if (transmittances == null || transmittances.Count == 0)
            {
                return null;
            }

            return transmittances.ToArray();
        }
    }
}