namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static System.Guid Guid(this string id)
        {
            if (string.IsNullOrEmpty(id))
                return System.Guid.Empty;

            System.Guid result;

            int index = id.IndexOf('_');
            if(index == -1)
            {
                if (!System.Guid.TryParse(id, out result))
                    return System.Guid.Empty;

                return result;
            }

            index++;
            if(index >= id.Length)
                return System.Guid.Empty;

            string value = id.Substring(index, id.Length - index);
            if (!System.Guid.TryParse(value, out result))
                return System.Guid.Empty;

            return result;
        }
    }
}