using System.IO;

namespace SAM.Analytical.gbXML
{
    public static partial class Create
    {
        public static AnalyticalModel AnalyticalModel(this string path, double tolerance = Core.Tolerance.Distance)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(gbXMLSerializer.gbXML));
            TextReader textReader = new StreamReader(path);
            object @object = xmlSerializer.Deserialize(textReader);
            if (@object == null)
                return null;

            gbXMLSerializer.gbXML gbXML = null;
            if (@object is gbXMLSerializer.gbXML)
                gbXML = (gbXMLSerializer.gbXML)@object;

            if (gbXML == null)
                return null;

            return gbXML.ToSAM(tolerance);
        }
    }
}