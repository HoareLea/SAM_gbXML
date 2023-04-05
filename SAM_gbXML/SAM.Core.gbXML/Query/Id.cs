using System;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static string Id(this IJSAMObject jSAMObject, Type type)
        {
            if (jSAMObject == null || type == null)
                return null;

            Guid guid = jSAMObject is ISAMObject ? ((ISAMObject)jSAMObject).Guid : System.Guid.NewGuid();

            return string.Format("{0}_{1}", type.Name, guid.ToString("N"));
        }

        public static string Id(this string text)
        {
            if(string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            string result = text;
            result = result.Replace(" ", "");
            result = result.Replace("/", "");
            result = result.Replace("\'", "");
            return result;
        }
    }
}