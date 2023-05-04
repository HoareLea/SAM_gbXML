using gbXMLSerializer;
using System.Collections.Generic;
using System.Linq;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Provides extension methods for querying gbXML objects.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Returns a list of all spaces in a building object.
        /// </summary>
        /// <param name="building">The building object.</param>
        /// <returns>A list of space objects.</returns>
        public static List<gbXMLSerializer.Space> Spaces(this Building building)
        {
            // Returns a list of spaces, if the building object is not null.
            // Otherwise, returns null.
            return building?.Spaces?.ToList();
        }
    }
}

