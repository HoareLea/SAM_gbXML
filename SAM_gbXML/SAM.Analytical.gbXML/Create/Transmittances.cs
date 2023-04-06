using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Create
    {
        public static gbXMLSerializer.Transmittance[] Transmittances(this Material material)
        {
            if(material == null)
            {
                return null;
            }

            List<gbXMLSerializer.Transmittance> transmittances = new List<gbXMLSerializer.Transmittance>();

            gbXMLSerializer.surfaceDescriptionEnum surfaceDescriptionEnum = gbXMLSerializer.surfaceDescriptionEnum.Both;

            if (material.TryGetValue(TransparentMaterialParameter.LightTransmittance, out double lightTransmittance))
            {
                gbXMLSerializer.Transmittance transmittance = new gbXMLSerializer.Transmittance();
                transmittance.surfaceType = surfaceDescriptionEnum;
                transmittance.type = gbXMLSerializer.radiationWavelengthTypeEnum.Visible;
                transmittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                transmittance.value = lightTransmittance;

                transmittances.Add(transmittance);
            }

            if (material.TryGetValue(TransparentMaterialParameter.SolarTransmittance, out double solarTransmittance))
            {
                gbXMLSerializer.Transmittance transmittance = new gbXMLSerializer.Transmittance();
                transmittance.surfaceType = surfaceDescriptionEnum;
                transmittance.type = gbXMLSerializer.radiationWavelengthTypeEnum.Solar;
                transmittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                transmittance.value = solarTransmittance;

                transmittances.Add(transmittance);
            }

            if (transmittances == null || transmittances.Count == 0)
            {
                return null;
            }

            return transmittances.ToArray();
        }

        public static gbXMLSerializer.Transmittance[] Transmittances(this ApertureConstruction apertureConstruction)
        {
            if (apertureConstruction == null)
            {
                return null;
            }

            List<gbXMLSerializer.Transmittance> transmittances = new List<gbXMLSerializer.Transmittance>();

            gbXMLSerializer.surfaceDescriptionEnum surfaceDescriptionEnum = gbXMLSerializer.surfaceDescriptionEnum.Both;

            if (apertureConstruction.TryGetValue(ApertureConstructionParameter.LightTransmittance, out double lightTransmittance))
            {
                gbXMLSerializer.Transmittance transmittance = new gbXMLSerializer.Transmittance();
                transmittance.surfaceType = surfaceDescriptionEnum;
                transmittance.type = gbXMLSerializer.radiationWavelengthTypeEnum.Visible;
                transmittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                transmittance.value = lightTransmittance;

                transmittances.Add(transmittance);
            }

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