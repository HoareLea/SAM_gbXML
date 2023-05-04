namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A collection of methods for converting construction and aperture construction objects to gbXML.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a SAM Analytical Construction object to a Construction object in gbXML format.
        /// </summary>
        /// <param name="construction">The SAM Analytical Construction object to convert.</param>
        /// <param name="tolerance">The tolerance to use for the conversion.</param>
        /// <returns>The resulting Construction object in gbXML format.</returns>
        public static gbXMLSerializer.Construction TogbXML(this Construction construction, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (construction == null)
                return null;

            gbXMLSerializer.Construction construction_gbXML = new gbXMLSerializer.Construction();

            // Set the ID of the gbXML construction object to the ID of the corresponding SAM Analytical construction object
            construction_gbXML.id = Core.gbXML.Query.Id(construction, typeof(gbXMLSerializer.Construction));

            // Set the name of the gbXML construction object to the name of the corresponding SAM Analytical construction object
            construction_gbXML.Name = construction.Name;

            // Set the layer ID of the gbXML construction object to the ID of the corresponding layer object
            construction_gbXML.LayerId = new gbXMLSerializer.LayerId[] { new gbXMLSerializer.LayerId() { layerIdRef = Core.gbXML.Query.Id(construction, typeof(gbXMLSerializer.Layer)) } };

            return construction_gbXML;
        }

        /// <summary>
        /// Converts a SAM Analytical ApertureConstruction object to a Construction object in gbXML format.
        /// </summary>
        /// <param name="apertureConstruction">The SAM Analytical ApertureConstruction object to convert.</param>
        /// <param name="tolerance">The tolerance to use for the conversion.</param>
        /// <returns>The resulting Construction object in gbXML format.</returns>
        public static gbXMLSerializer.Construction TogbXML(this ApertureConstruction apertureConstruction, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (apertureConstruction == null)
                return null;

            gbXMLSerializer.Construction construction_gbXML = new gbXMLSerializer.Construction();

            // Set the ID of the gbXML construction object to the ID of the corresponding SAM Analytical aperture construction object
            construction_gbXML.id = Core.gbXML.Query.Id(apertureConstruction, typeof(gbXMLSerializer.Construction));

            // Set the name of the gbXML construction object to the name of the corresponding SAM Analytical aperture construction object
            construction_gbXML.Name = apertureConstruction.Name;

            return construction_gbXML;
        }

    }
}
