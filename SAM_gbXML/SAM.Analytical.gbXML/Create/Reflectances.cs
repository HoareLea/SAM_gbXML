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

            gbXMLSerializer.surfaceDescriptionEnum surfaceDescriptionEnum = gbXMLSerializer.surfaceDescriptionEnum.Both;

            System.Enum @enum;

            @enum = material is OpaqueMaterial ? OpaqueMaterialParameter.ExternalEmissivity : TransparentMaterialParameter.ExternalEmissivity;
            if (material.TryGetValue(@enum, out double externalEmissivity))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceType = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.ExtIR;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = externalEmissivity;

                reflectances.Add(reflectance);
            }

            @enum = material is OpaqueMaterial ? OpaqueMaterialParameter.ExternalLightReflectance : TransparentMaterialParameter.ExternalLightReflectance;
            if (material.TryGetValue(@enum, out double externalLightReflectance))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceType = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.ExtVisible;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = externalLightReflectance;

                reflectances.Add(reflectance);
            }

            @enum = material is OpaqueMaterial ? OpaqueMaterialParameter.ExternalSolarReflectance : TransparentMaterialParameter.ExternalSolarReflectance;
            if (material.TryGetValue(@enum, out double externalSolarReflectance))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceType = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.ExtSolar;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = externalSolarReflectance;

                reflectances.Add(reflectance);
            }

            @enum = material is OpaqueMaterial ? OpaqueMaterialParameter.InternalEmissivity : TransparentMaterialParameter.InternalEmissivity;
            if (material.TryGetValue(@enum, out double internalEmissivity))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceType = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.IntIR;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = internalEmissivity;

                reflectances.Add(reflectance);
            }

            @enum = material is OpaqueMaterial ? OpaqueMaterialParameter.InternalLightReflectance : TransparentMaterialParameter.InternalLightReflectance;
            if (material.TryGetValue(@enum, out double internalLightReflectance))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceType = surfaceDescriptionEnum;
                reflectance.type = gbXMLSerializer.reflectanceTypeEnum.IntVisible;
                reflectance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                reflectance.value = internalLightReflectance;

                reflectances.Add(reflectance);
            }

            @enum = material is OpaqueMaterial ? OpaqueMaterialParameter.InternalSolarReflectance : TransparentMaterialParameter.InternalSolarReflectance;
            if (material.TryGetValue(@enum, out double internalSolarReflectance))
            {
                gbXMLSerializer.Reflectance reflectance = new gbXMLSerializer.Reflectance();
                reflectance.surfaceType = surfaceDescriptionEnum;
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