using System.IO;
using System.Xml.Serialization;

namespace SAM.Core.gbXML
{
    /// <summary>
    /// Provides static methods for creating and serializing gbXML objects.
    /// </summary>
    public static partial class Create
    {
        /// <summary>
        /// Provides methods for creating and serializing gbXML objects.
        /// </summary>
        /// <param name="path">The path to the gbXML file.</param>
        /// <returns>The deserialized gbXML object, or null if the file cannot be read or deserialized.</returns>
        public static gbXMLSerializer.gbXML gbXML(this string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            if (!File.Exists(path))
                return null;
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(gbXMLSerializer.gbXML));
            if (xmlSerializer == null)
                return null;

            gbXMLSerializer.gbXML result = null;

            try
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    result = xmlSerializer.Deserialize(streamReader) as gbXMLSerializer.gbXML;
                    streamReader.Close();
                }
            }
            catch
            {

            }

            return result;
        }

        /// <summary>
        /// Serializes the given gbXML object to a file at the given path.
        /// </summary>
        /// <param name="gbXML">The gbXML object to serialize.</param>
        /// <param name="path">The path to write the serialized gbXML to.</param>
        /// <returns>True if the serialization was successful, false otherwise.</returns>
        public static bool gbXML(this gbXMLSerializer.gbXML gbXML, string path)
        {
            if (gbXML == null || string.IsNullOrWhiteSpace(path))
                return false;
            
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(gbXMLSerializer.gbXML));

            try
            {
                using (TextWriter textWriter = new StreamWriter(path))
                {
                    xmlSerializer.Serialize(textWriter, gbXML);
                    textWriter.Close();
                }

                return true;
            }
            catch
            {
                
            }

            return false;
        }
    }
}