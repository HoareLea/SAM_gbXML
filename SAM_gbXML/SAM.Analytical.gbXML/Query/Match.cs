using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static Space Match(this gbXMLSerializer.Space space, IEnumerable<Space> spaces)
        {
            if (space == null || spaces == null)
                return null;

            string id = space.cadid?.id?.Trim();
            if (string.IsNullOrWhiteSpace(id))
                return null;

            foreach(Space space_SAM in spaces)
            {
                int elementId = -1;
                if (!space_SAM.TryGetValue<int>("ElementId", out elementId))
                    continue;

                if (elementId == -1)
                    continue;

                if (id.Equals(elementId.ToString()))
                    return space_SAM;
            }

            return null;
        }
    }
}