using gbXMLSerializer;
using System.Collections.Generic;
using System.Linq;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static List<gbXMLSerializer.Space> Spaces(this Building building)
        {
            return building?.Spaces?.ToList();
        }
    }
}