using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static AdjacentSpaceId AdjacentSpaceId(this Space space)
        {
            if (space == null)
                return null;

            AdjacentSpaceId adjacentSpaceId = new AdjacentSpaceId();
            adjacentSpaceId.spaceIdRef = Core.gbXML.Query.Id(space, typeof(gbXMLSerializer.Space));
            return adjacentSpaceId;
        }
    }
}