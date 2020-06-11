using System;
using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static CreatedBy CreatedBy(this Guid cADModelId)
        {
            CreatedBy createdBy = new CreatedBy();
            createdBy.CADModelId = cADModelId.ToString();
            createdBy.date = DateTime.Now;
            createdBy.personId = Environment.UserName;
            createdBy.programId = "SAM_gbXML";

            return createdBy;
        }
    }
}