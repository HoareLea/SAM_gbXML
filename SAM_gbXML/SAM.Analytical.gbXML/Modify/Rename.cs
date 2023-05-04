namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// This static class contains methods for modifying gbXML data.
    /// </summary>
    public static partial class Modify
    {
        /// <summary>
        /// Renames a space in gbXML to the specified name.
        /// </summary>
        /// <param name="space">The space to rename.</param>
        /// <param name="name">The new name for the space.</param>
        /// <returns>True if the rename was successful, false otherwise.</returns>
        public static bool Rename(this gbXMLSerializer.Space space, string name)
        {
            // If the new name is null, empty or whitespace, return false.
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // Set the space's Name property to the new name and return true to indicate success.
            space.Name = name;
            return true;
        }
    }
}
