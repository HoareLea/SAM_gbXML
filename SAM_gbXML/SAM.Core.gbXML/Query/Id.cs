using System;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static string Id(this ISAMObject sAMObject, Type type)
        {
            if (sAMObject == null || type == null)
                return null;

            return string.Format("{0}[1]", type.Name, sAMObject.Guid.ToString("N"));
        }
    }
}