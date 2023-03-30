using SAM.Core.Attributes;
using System.ComponentModel;

namespace SAM.Analytical.gbXML
{
    [AssociatedTypes(typeof(Panel)), Description("Panel Parameter")]
    public enum PanelParameter
    {
        [ParameterProperties("Id", "Id"), ParameterValue(Core.ParameterType.String)] Id,
    }
}
