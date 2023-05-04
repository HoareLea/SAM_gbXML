using System.Collections.Generic;
using System.Linq;
using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Modify
    {
        /// <summary>
        /// Renames spaces in a gbXML file based on the specified collection of Space objects.
        /// </summary>
        /// <param name="path">The path to the gbXML file.</param>
        /// <param name="spaces">The collection of Space objects to use for renaming.</param>
        /// <returns>True if the renaming was successful, false otherwise.</returns>
        public static bool RenameSpaces(this string path, IEnumerable<Space> spaces)
        {
            // If the collection of spaces is null or empty, return false.
            if (spaces == null || spaces.Count() == 0)
                return false;

            // Load the gbXML file into a gbXML object.
            gbXMLSerializer.gbXML gbXML = Core.gbXML.Create.gbXML(path);

            // Get an array of buildings in the gbXML object.
            Building[] buildings = gbXML?.Campus?.Buildings;

            // If there are no buildings in the gbXML object, return false.
            if (buildings == null || buildings.Length == 0)
                return false;

            // Loop through each building in the gbXML object.
            foreach (Building building in buildings)
            {
                // Get an array of spaces in the current building.
                gbXMLSerializer.Space[] spaces_gbXML = building.Spaces;

                // If there are no spaces in the current building, skip to the next building.
                if (spaces_gbXML == null || spaces_gbXML.Length == 0)
                    continue;

                // Loop through each space in the current building.
                foreach (gbXMLSerializer.Space space_gbXML in spaces_gbXML)
                {
                    // Try to find a matching Space object in the specified collection.
                    Space space_SAM = space_gbXML.Match(spaces);

                    // If no matching Space object was found, skip to the next space.
                    if (space_SAM == null)
                        continue;

                    // Set the space's Name property to the name of the matching Space object.
                    space_gbXML.Name = space_SAM.Name;
                }
            }

            // Save the modified gbXML object back to the original file and return true to indicate success.
            return Core.gbXML.Create.gbXML(gbXML, path);
        }
    }
}
