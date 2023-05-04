using SAM.Core.Attributes;
using System.ComponentModel;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// This enum represents various parameters that can be associated with a space in the gbXML format.
    /// </summary>
    [AssociatedTypes(typeof(Space)), Description("Space Parameter")]
    public enum SpaceParameter
    {
        /// <summary>
        /// This parameter represents the unique identifier of the space.
        /// </summary>
        [ParameterProperties("Id", "Id"), ParameterValue(Core.ParameterType.String)] Id,
    }
}

