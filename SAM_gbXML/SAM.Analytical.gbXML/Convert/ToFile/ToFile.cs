using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts an AnalyticalModel object to a gbXML file and saves it to the specified path.
        /// </summary>
        /// <param name="analyticalModel">The AnalyticalModel object to convert to gbXML.</param>
        /// <param name="path">The path where the gbXML file will be saved.</param>
        /// <param name="silverSpacing">The maximum distance between two points before they are considered equal.</param>
        /// <param name="tolerance">The tolerance used for comparison of double values.</param>
        /// <returns>True if the gbXML file was created successfully, false otherwise.</returns>
        public static bool ToFile(this AnalyticalModel analyticalModel, string path, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.MicroDistance)
        {
            // Check if analyticalModel and path are not null or empty
            if (analyticalModel == null || string.IsNullOrEmpty(path))
            {
                return false;
            }

            // Convert analyticalModel to gbXML using the specified silverSpacing and tolerance
            gbXMLSerializer.gbXML gbXML = analyticalModel.TogbXML(silverSpacing, tolerance);

            // Check if gbXML is null
            if (gbXML == null)
            {
                return false;
            }

            // Save gbXML to the specified path using the Core.gbXML.Create.gbXML method
            return Core.gbXML.Create.gbXML(gbXML, path);
        }
    }
}
