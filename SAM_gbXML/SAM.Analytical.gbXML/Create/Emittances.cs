using SAM.Core;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Create
    {
        public static gbXMLSerializer.Emittance[] Emittances(this Material material)
        {
            if(material == null)
            {
                return null;
            }

            List<gbXMLSerializer.Emittance> emittances = new List<gbXMLSerializer.Emittance>();

            if (material.TryGetValue(TransparentMaterialParameter.ExternalEmissivity, out double externalEmissivity))
            {
                gbXMLSerializer.Emittance emittance = new gbXMLSerializer.Emittance();
                emittance.type = gbXMLSerializer.emittanceTypeEnum.ExtIR;
                emittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                emittance.value = externalEmissivity;

                emittances.Add(emittance);
            }

            if (material.TryGetValue(TransparentMaterialParameter.InternalEmissivity, out double internalEmissivity))
            {
                gbXMLSerializer.Emittance emittance = new gbXMLSerializer.Emittance();
                emittance.type = gbXMLSerializer.emittanceTypeEnum.IntIR;
                emittance.unit = gbXMLSerializer.unitlessUnitEnum.Fraction;
                emittance.value = internalEmissivity;

                emittances.Add(emittance);
            }

            if (emittances == null || emittances.Count == 0)
            {
                return null;
            }

            return emittances.ToArray();
        }
    }
}