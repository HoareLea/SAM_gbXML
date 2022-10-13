using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static CADObjectId CADObjectId(this Panel panel, int cADObjectIdSufix = -1)
        {
            if (panel == null)
                return null;

            string uniqueName = Analytical.Query.UniqueName(panel, cADObjectIdSufix);
            if (string.IsNullOrEmpty(uniqueName))
                return null;

            CADObjectId cADObjectId = new CADObjectId();
            cADObjectId.id = uniqueName;

            return cADObjectId;
        }

        public static CADObjectId CADObjectId(this Aperture aperture, int cADObjectIdSufix = -1)
        {
            if (aperture == null)
                return null;

            string uniqueName = Analytical.Query.UniqueName(aperture, cADObjectIdSufix);
            if (string.IsNullOrEmpty(uniqueName))
            {
                return null;
            }

            if(Analytical.Query.UniqueNameDecomposition(uniqueName, out string prefix, out string name, out System.Guid? guid, out int id))
            {
                uniqueName = string.Empty;
                if(!string.IsNullOrWhiteSpace(prefix))
                {
                    uniqueName += prefix + ":";
                }

                if(!string.IsNullOrWhiteSpace(name))
                {
                    uniqueName += " " + name;
                }

                if(guid != null && guid.HasValue)
                {
                    uniqueName += " " + guid.ToString();
                }

                if(id != -1)
                {
                    uniqueName += string.Format(" [{0}]", id);
                }

                uniqueName = uniqueName.Trim();

            }

            CADObjectId cADObjectId = new CADObjectId();
            cADObjectId.id = uniqueName;

            return cADObjectId;
        }
    }
}