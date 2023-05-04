namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Query class with extension methods for gbXMLSerializer.gbXML
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Returns the material with the specified ID from the gbXML document
        /// </summary>
        /// <param name="gbXML">The gbXML document</param>
        /// <param name="id">The ID of the material to retrieve</param>
        /// <returns>The material with the specified ID, or null if not found</returns>
        public static gbXMLSerializer.Material Material(this gbXMLSerializer.gbXML gbXML, string id)
        {
            // Return null if either the gbXML document or the ID is null or empty
            if (gbXML == null || string.IsNullOrEmpty(id))
            {
                return null;
            }

            // Get an array of all the materials in the gbXML document
            gbXMLSerializer.Material[] materials = gbXML.Materials;

            // Return null if no materials are found
            if (materials == null)
            {
                return null;
            }

            // Iterate through each material in the array and check if its ID matches the specified ID
            foreach (gbXMLSerializer.Material material in materials)
            {
                if (id.Equals(material?.id)) // Use the null conditional operator to avoid a NullReferenceException if material is null
                {
                    return material;
                }
            }

            // Return null if no material with the specified ID is found
            return null;
        }
    }
}
