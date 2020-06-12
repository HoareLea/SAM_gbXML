using System;
using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static ModifiedBy ModifiedBy()
        {
            ModifiedBy modifiedBy = new ModifiedBy();
            modifiedBy.date = DateTime.Now;
            modifiedBy.personId = Environment.UserName;
            modifiedBy.programId = "SAM_gbXML";

            return modifiedBy;
        }
    }
}