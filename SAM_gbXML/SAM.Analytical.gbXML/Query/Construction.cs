using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static Construction Construction(this IEnumerable<Construction> constructions, string id)
        {
            if(constructions == null || string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            foreach(Construction construction in constructions)
            {
                if(construction == null || !construction.TryGetValue(ConstructionParameter.Id, out string id_Construction) || string.IsNullOrWhiteSpace(id_Construction))
                {
                    continue;
                }

                if(id.Equals(id_Construction))
                {
                    return construction;
                }
            }

            return null;
        }
    }
}