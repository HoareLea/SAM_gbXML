using SAM.Core.Attributes;
using System.ComponentModel;

namespace SAM.Analytical.gbXML
{
    [AssociatedTypes(typeof(Construction)), Description("Construction Parameter")]
    public enum ConstructionParameter
    {
        [ParameterProperties("Id", "Id"), ParameterValue(Core.ParameterType.String)] Id,
    }
}
