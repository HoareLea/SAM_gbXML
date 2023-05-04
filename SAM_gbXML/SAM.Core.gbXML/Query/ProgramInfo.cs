using System;
using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    /// <summary>
    /// Provides methods for querying gbXML objects.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Returns information about the program that created the gbXML file.
        /// </summary>
        /// <returns>The program information.</returns>
        public static ProgramInfo ProgramInfo()
        {
            // Create a new instance of the ProgramInfo class.
            ProgramInfo programInfo = new ProgramInfo();

            // Set the values of the programInfo object's properties.
            programInfo.CompanyName = "Hoare Lea, Michal Dengusiak and Jakub Ziolkowski";
            programInfo.id = "SAM_gbXML";
            programInfo.Platform = Environment.OSVersion.ToString();
            programInfo.ProductName = "Autodesk Revit 2020 BEES";
            programInfo.Version = "2020 20191031_1115(x64)";

            // Return the programInfo object.
            return programInfo;
        }
    }
}
