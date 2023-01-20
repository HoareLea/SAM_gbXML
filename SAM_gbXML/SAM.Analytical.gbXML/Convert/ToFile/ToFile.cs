using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static bool ToFile(this AnalyticalModel analyticalModel, string path, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (analyticalModel == null || string.IsNullOrEmpty(path))
            {
                return false;
            }

            gbXMLSerializer.gbXML gbXML = analyticalModel.TogbXML(silverSpacing, tolerance);
            if (gbXML == null)
            {
                return false;
            }
                

            return Core.gbXML.Create.gbXML(gbXML, path);
        }

    }
}
