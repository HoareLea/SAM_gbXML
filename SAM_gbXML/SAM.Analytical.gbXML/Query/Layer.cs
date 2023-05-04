namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A collection of static methods for querying gbXML data.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Gets the gbXML layer with the specified ID.
        /// </summary>
        /// <param name="gbXML">The gbXML object to query.</param>
        /// <param name="id">The ID of the layer to get.</param>
        /// <returns>The gbXML layer with the specified ID, or null if the layer is not found.</returns>
        public static gbXMLSerializer.Layer Layer(this gbXMLSerializer.gbXML gbXML, string id)
        {
            // Check if the gbXML object or ID is null or empty
            if (gbXML == null || string.IsNullOrEmpty(id))
            {
                return null;
            }

            // Get the layers from the gbXML object
            gbXMLSerializer.Layer[] layers = gbXML.Layers;

            // If there are no layers, return null
            if (layers == null)
            {
                return null;
            }

            // Loop through the layers and find the one with the specified ID
            foreach (gbXMLSerializer.Layer layer in layers)
            {
                if (id.Equals(layer?.id))
                {
                    return layer;
                }
            }

            // If the layer is not found, return null
            return null;
        }
    }
}
