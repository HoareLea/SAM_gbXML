using System.IO;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Static class for creating building models from gbXML files.
    /// </summary>
    public static partial class Create
    {
        /// <summary>
        /// Create a BuildingModel from a gbXML file.
        /// </summary>
        /// <param name="path">Path to the gbXML file.</param>
        /// <param name="tolerance">Tolerance for the conversion (optional, default is Core.Tolerance.Distance).</param>
        /// <returns>A BuildingModel object.</returns>
        public static BuildingModel BuildingModel(this string path, double tolerance = Core.Tolerance.Distance)
        {
            // Create an XmlSerializer for the gbXML type.
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(gbXMLSerializer.gbXML));

            // Open the gbXML file for reading.
            TextReader textReader = new StreamReader(path);

            // Deserialize the gbXML file into an object.
            object @object = xmlSerializer.Deserialize(textReader);

            // If the deserialization fails, return null.
            if (@object == null)
                return null;

            // Cast the deserialized object to gbXMLSerializer.gbXML type.
            gbXMLSerializer.gbXML gbXML = null;
            if (@object is gbXMLSerializer.gbXML)
                gbXML = (gbXMLSerializer.gbXML)@object;

            // If the cast fails, return null.
            if (gbXML == null)
                return null;

            // Convert the gbXML object to a SAM BuildingModel object.
            return gbXML.ToSAM_BuildingModel(tolerance);
        }
    }
}
