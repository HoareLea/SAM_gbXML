namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static System.Guid Guid(this string id)
        {
            if (string.IsNullOrEmpty(id))
                return System.Guid.Empty;

            System.Guid result;

            int index_Start = id.IndexOf('[');
            if(index_Start == -1)
            {
                if (!System.Guid.TryParse(id, out result))
                    return System.Guid.Empty;

                return result;
            }

            int index_End = id.IndexOf(']', index_Start);
            if (index_End == -1)
                return System.Guid.Empty;

            string value = id.Substring(index_Start, index_End - index_Start - 1);
            if (!System.Guid.TryParse(value, out result))
                return System.Guid.Empty;

            return result;
        }
    }
}