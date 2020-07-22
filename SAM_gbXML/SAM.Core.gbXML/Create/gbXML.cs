using System.IO;
using System.Xml.Serialization;

namespace SAM.Core.gbXML
{
    public static partial class Create
    {
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