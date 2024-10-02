using SAM.Analytical;
using System.IO;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Provides static methods for creating and serializing gbXML objects.
    /// </summary>
    public static partial class Create
    {
        public static bool gbXML(this AnalyticalModel analyticalModel, string path, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.MicroDistance)
        {
            if(analyticalModel == null || string.IsNullOrWhiteSpace(path) || !Directory.Exists(Path.GetDirectoryName(path)))
            {
                return false;
            }

            gbXMLSerializer.gbXML gbXML = Convert.TogbXML(analyticalModel, silverSpacing, tolerance);
            if(gbXML == null)
            {
                return false;
            }

            return Core.gbXML.Create.gbXML(gbXML, path);
        }
    }
}