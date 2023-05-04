using SAM.Core.Attributes; 
using System.ComponentModel; 

namespace SAM.Analytical.gbXML // Declaring a namespace for the code
{
    /// <summary>
    /// An enumeration representing the parameters associated with a construction object in a gbXML file.
    /// </summary>
    [AssociatedTypes(typeof(Construction)), Description("Construction Parameter")]
    public enum ConstructionParameter
    {
        /// <summary>
        /// The ID parameter of the construction object.
        /// </summary>
        [ParameterProperties("Id", "Id"), ParameterValue(Core.ParameterType.String)]
        Id,
    }
}

