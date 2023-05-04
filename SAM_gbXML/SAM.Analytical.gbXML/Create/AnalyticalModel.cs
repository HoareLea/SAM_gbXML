using SAM.Core;
using System.IO;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Static class that provides methods for creating an <see cref="AnalyticalModel"/> instance from a gbXML file.
    /// </summary>
    public static partial class Create
    {
        /// <summary>
        /// Creates an <see cref="AnalyticalModel"/> instance from a gbXML file.
        /// </summary>
        /// <param name="path">The file path of the gbXML file.</param>
        /// <param name="silverSpacing">The silver spacing to use when converting the gbXML file to an <see cref="AnalyticalModel"/>. Defaults to <see cref="Tolerance.MacroDistance"/>.</param>
        /// <param name="tolerance">The tolerance to use when converting the gbXML file to an <see cref="AnalyticalModel"/>. Defaults to <see cref="Tolerance.Distance"/>.</param>
        /// <returns>An <see cref="AnalyticalModel"/> instance created from the gbXML file. Returns null if the file could not be read or if the conversion failed.</returns>
        public static AnalyticalModel AnalyticalModel(this string path, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.Distance)
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

            return gbXML.ToSAM(silverSpacing, tolerance);
        }
    }
}
