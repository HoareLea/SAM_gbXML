using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        /// <summary>
        /// Matches a gbXML Space object to a collection of Space objects.
        /// </summary>
        /// <param name="space">The gbXML Space object to match.</param>
        /// <param name="spaces">The collection of Space objects to match against.</param>
        /// <returns>The first Space object in the collection that matches the gbXML Space object, or null if no match is found.</returns>
        public static Space Match(this gbXMLSerializer.Space space, IEnumerable<Space> spaces)
        {
            // Check if either argument is null, and return null if so
            if (space == null || spaces == null)
                return null;

            // Get the ID of the gbXML Space object
            string id = space.cadid?.id?.Trim();
            // Return null if the ID is null, empty or whitespace
            if (string.IsNullOrWhiteSpace(id))
                return null;

            // Loop through the collection of Space objects
            foreach (Space space_SAM in spaces)
            {
                // Try to get the ElementId property of the Space object
                int elementId = -1;
                if (!space_SAM.TryGetValue("ElementId", out elementId))
                    continue; // If the property is not found, continue to the next Space object

                if (elementId == -1)
                    continue; // If the ElementId is -1, continue to the next Space object

                // If the ElementId matches the ID of the gbXML Space object, return the Space object
                if (id.Equals(elementId.ToString()))
                    return space_SAM;
            }

            // If no match is found, return null
            return null;
        }
    }
}
