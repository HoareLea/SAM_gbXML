// Using statement to import namespaces containing classes used in this file
using SAM.Core.Attributes;
using System.ComponentModel;

// Namespace declaration for the gbXML analysis module
namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// An enum type used to define different types of panel parameters for gbXML analysis.
    /// </summary>
    [AssociatedTypes(typeof(Panel)), Description("Panel Parameter")]
    public enum PanelParameter
    {
        /// <summary>
        /// The ID of the panel.
        /// </summary>
        [ParameterProperties("Id", "Id"), ParameterValue(Core.ParameterType.String)] Id,
    }
}

