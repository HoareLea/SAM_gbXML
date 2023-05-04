using gbXMLSerializer;

// Define a namespace for the extension methods
namespace SAM.Analytical.gbXML
{
    // Define a static partial class called Query that contains extension methods for Space objects
    public static partial class Query
    {
        /// <summary>
        /// Returns the identifier of the adjacent space of a given space.
        /// </summary>
        /// <param name="space">The space to retrieve the adjacent space identifier from.</param>
        /// <returns>An AdjacentSpaceId object containing the adjacent space identifier.</returns>
        public static AdjacentSpaceId AdjacentSpaceId(this Space space)
        {
            // If the input space is null, return null
            if (space == null)
                return null;

            // Create a new AdjacentSpaceId object to store the adjacent space identifier
            AdjacentSpaceId adjacentSpaceId = new AdjacentSpaceId();

            // Retrieve the identifier of the adjacent space using the gbXML Query.Id method and store it in the AdjacentSpaceId object
            adjacentSpaceId.spaceIdRef = Core.gbXML.Query.Id(space, typeof(gbXMLSerializer.Space));

            // Return the AdjacentSpaceId object containing the adjacent space identifier
            return adjacentSpaceId;
        }
    }
}
