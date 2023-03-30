using SAM.Core.Attributes;
using System.ComponentModel;

namespace SAM.Analytical.gbXML
{
    [AssociatedTypes(typeof(Space)), Description("Space Parameter")]
    public enum SpaceParameter
    {
        [ParameterProperties("Id", "Id"), ParameterValue(Core.ParameterType.String)] Id,
    }
}
