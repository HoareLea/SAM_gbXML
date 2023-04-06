using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Create
    {
        public static gbXMLSerializer.Reflectance[] Reflectances(this Material material)
        {
            if(material == null)
            {
                return null;
            }

            List<gbXMLSerializer.Reflectance> reflectances = new List<gbXMLSerializer.Reflectance>();

            gbXMLSerializer.surfaceDescriptionEnum surfaceDescriptionEnum = gbXMLSerializer.surfaceDescriptionEnum.One;

            if (material.TryGetValue(OpaqueMaterialParameter.ExternalEmissivity, out double externalEmissivity))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceDescriptionEnum = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.ExtIR;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = externalEmissivity;

                reflectances.Add(reflectance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.ExternalLightReflectance, out double externalLightReflectance))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceDescriptionEnum = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.ExtVisible;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = externalLightReflectance;

                reflectances.Add(reflectance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.ExternalSolarReflectance, out double externalSolarReflectance))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceDescriptionEnum = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.ExtSolar;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = externalSolarReflectance;

                reflectances.Add(reflectance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.InternalEmissivity, out double internalEmissivity))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceDescriptionEnum = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.IntIR;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = internalEmissivity;

                reflectances.Add(reflectance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.InternalLightReflectance, out double internalLightReflectance))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceDescriptionEnum = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.IntVisible;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = internalLightReflectance;

                reflectances.Add(reflectance);
            }

            if (material.TryGetValue(OpaqueMaterialParameter.InternalSolarReflectance, out double internalSolarReflectance))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceDescriptionEnum = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.IntSolar;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = internalSolarReflectance;

                reflectances.Add(reflectance);
            }

            if (reflectances == null || reflectances.Count == 0)
            {
                return null;
            }

            return reflectances.ToArray();
        }
    }
}