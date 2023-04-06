using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Create
    {
        public static gbXMLSerializer.Absorptance[] Absorptances(this Material material)
        {
            if(material == null)
            {
                return null;
            }

            List<gbXMLSerializer.Absorptance> absorptances = new List<gbXMLSerializer.Absorptance>();

            gbXMLSerializer.surfaceDescriptionEnum surfaceDescriptionEnum = gbXMLSerializer.surfaceDescriptionEnum.Both;

            if (material.TryGetValue(OpaqueMaterialParameter.ExternalEmissivity, out double externalEmissivity))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.surfaceType = surfaceDescriptionEnum;
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.ExtIR;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - externalEmissivity;

                absorptances.Add(absorptance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.ExternalLightReflectance, out double externalLightReflectance))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.surfaceType = surfaceDescriptionEnum;
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.ExtVisible;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - externalLightReflectance;

                absorptances.Add(absorptance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.ExternalSolarReflectance, out double externalSolarReflectance))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.surfaceType = surfaceDescriptionEnum;
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.ExtSolar;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - externalSolarReflectance;

                absorptances.Add(absorptance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.InternalEmissivity, out double internalEmissivity))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.surfaceType = surfaceDescriptionEnum;
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.IntIr;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - internalEmissivity;

                absorptances.Add(absorptance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.InternalLightReflectance, out double internalLightReflectance))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.surfaceType = surfaceDescriptionEnum;
                absorptance.type = gbXMLSerializer.absorptanceUnitEnum.IntVisible;
                absorptance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                absorptance.value = 1 - internalLightReflectance;

                absorptances.Add(absorptance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.InternalSolarReflectance, out double internalSolarReflectance))
            {
                gbXMLSerializer.Absorptance absorptance = new gbXMLSerializer.Absorptance();
                absorptance.surfaceType = surfaceDescriptionEnum;
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