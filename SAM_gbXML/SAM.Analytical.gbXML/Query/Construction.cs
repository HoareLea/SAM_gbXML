using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        /// <summary>
        /// Finds a construction object by its ID within a collection of constructions.
        /// </summary>
        /// <param name="constructions">A collection of constructions to search through.</param>
        /// <param name="id">The ID of the construction object to find.</param>
        /// <returns>The construction object with the specified ID, or null if not found.</returns>
        public static Construction Construction(this IEnumerable<Construction> constructions, string id)
        {
            // Check if the input arguments are valid
            if (constructions == null || string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            // Loop through each construction object in the collection
            foreach (Construction construction in constructions)
            {
                // Check if the construction object is valid and has an ID property
                if (construction == null || !construction.TryGetValue(ConstructionParameter.Id, out string id_Construction) || string.IsNullOrWhiteSpace(id_Construction))
                {
                    // If not, skip to the next object
                    continue;
                }

                // If the construction object's ID matches the input ID, return it
                if (id.Equals(id_Construction))
                {
                    return construction;
                }
            }

            // If no matching construction object was found, return null
            return null;
        }
    }
}
