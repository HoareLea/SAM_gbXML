using System.Collections.Generic;
using System.Linq;
using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Modify
    {
        public static bool RenameSpaces(this string path, IEnumerable<Space> spaces)
        {
            if (spaces == null || spaces.Count() == 0)
                return false;

            gbXMLSerializer.gbXML gbXML = Core.gbXML.Create.gbXML(path);

            Building[] buildings = gbXML?.Campus?.Buildings;
            if (buildings == null || buildings.Length == 0)
                return false;

            foreach(Building building in buildings)
            {
                gbXMLSerializer.Space[] spaces_gbXML = building.Spaces;
                if (spaces_gbXML == null || spaces_gbXML.Length == 0)
                    continue;

                foreach(gbXMLSerializer.Space space_gbXML in spaces_gbXML)
                {
                    Space space_SAM = space_gbXML.Match(spaces);
                    if (space_SAM == null)
                        continue;

                    space_gbXML.Name = space_SAM.Name;
                }
            }

            return Core.gbXML.Create.gbXML(gbXML, path);
        }
    }
}