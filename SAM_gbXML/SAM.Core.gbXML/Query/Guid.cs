namespace SAM.Core.gbXML
{
    /// <summary>
    /// A collection of utility functions for querying gbXML objects.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Extracts the GUID from an ID string.
        /// </summary>
        /// <param name="id">The ID string.</param>
        /// <returns>The GUID extracted from the ID string, or <see cref="System.Guid.Empty"/> if the ID is null, empty, or invalid.</returns>
        public static System.Guid Guid(this string id)
        {
            if (string.IsNullOrEmpty(id))
                return System.Guid.Empty;

            System.Guid result;

            int index = id.IndexOf('_');
            if (index == -1)
            {
                if (!System.Guid.TryParse(id, out result))
                    return System.Guid.Empty;

                return result;
            }

            index++;
            if (index >= id.Length)
                return System.Guid.Empty;

            string value = id.Substring(index, id.Length - index);
            if (!System.Guid.TryParse(value, out result))
                return System.Guid.Empty;

            return result;
        }
    }
}
