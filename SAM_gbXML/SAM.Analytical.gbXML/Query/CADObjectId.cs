using gbXMLSerializer;

// Define a namespace for the extension methods
namespace SAM.Analytical.gbXML
{
    // Define a static partial class to contain the extension methods
    public static partial class Query
    {
        /// <summary>
        /// Returns a CADObjectId for the specified Panel object, with an optional suffix.
        /// </summary>
        /// <param name="panel">The Panel object to generate a CADObjectId for.</param>
        /// <param name="cADObjectIdSufix">An optional suffix to append to the generated CADObjectId.</param>
        /// <returns>A CADObjectId object representing the specified Panel object.</returns>
        public static CADObjectId CADObjectId(this IPanel panel, int cADObjectIdSufix = -1)
        {
            // Check for null input
            if (panel == null)
                return null;

            // Generate a unique name for the object
            string uniqueName = Analytical.Query.UniqueName(panel, cADObjectIdSufix);

            // Check for empty or null name
            if (string.IsNullOrEmpty(uniqueName))
                return null;

            // Create a new CADObjectId with the unique name
            CADObjectId cADObjectId = new CADObjectId();
            cADObjectId.id = uniqueName;

            // Return the CADObjectId
            return cADObjectId;
        }

        /// <summary>
        /// Returns a CADObjectId for the specified Aperture object, with an optional suffix.
        /// </summary>
        /// <param name="aperture">The Aperture object to generate a CADObjectId for.</param>
        /// <param name="cADObjectIdSufix">An optional suffix to append to the generated CADObjectId.</param>
        /// <returns>A CADObjectId object representing the specified Aperture object.</returns>
        public static CADObjectId CADObjectId(this Aperture aperture, int cADObjectIdSufix = -1)
        {
            // Check for null input
            if (aperture == null)
                return null;

            // Generate a unique name for the object
            string uniqueName = Analytical.Query.UniqueName(aperture, cADObjectIdSufix);

            // Check for empty or null name
            if (string.IsNullOrEmpty(uniqueName))
            {
                return null;
            }

            // Decompose the unique name into its parts (prefix, name, GUID, and ID)
            if (Analytical.Query.UniqueNameDecomposition(uniqueName, out string prefix, out string name, out System.Guid? guid, out int id))
            {
                // Rebuild the unique name with appropriate formatting
                uniqueName = string.Empty;
                if (!string.IsNullOrWhiteSpace(prefix))
                {
                    uniqueName += prefix + ":";
                }

                if (!string.IsNullOrWhiteSpace(name))
                {
                    uniqueName += " " + name;
                }

                if (guid != null && guid.HasValue)
                {
                    uniqueName += " " + guid.ToString();
                }

                if (id != -1)
                {
                    uniqueName += string.Format(" [{0}]", id);
                }

                uniqueName = uniqueName.Trim();
            }

            // Create a new CADObjectId with the unique name
            CADObjectId cADObjectId = new CADObjectId();
            cADObjectId.id = uniqueName;

            // Return the CADObjectId
            return cADObjectId;
        }
    }
}
