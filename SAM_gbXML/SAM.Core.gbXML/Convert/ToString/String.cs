using System.IO;

namespace SAM.Core.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts the gbXML object to its XML representation in string format.
        /// </summary>
        /// <param name="gbXML">The gbXML object to convert.</param>
        /// <returns>A string containing the XML representation of the gbXML object.</returns>
        public static string ToString(this gbXMLSerializer.gbXML gbXML)
        {
            if (gbXML == null)
                return null;

            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(gbXMLSerializer.gbXML));

            string result = null;
            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, gbXML);
                result = textWriter.ToString();
            }

            return result;
        }

    }
}
