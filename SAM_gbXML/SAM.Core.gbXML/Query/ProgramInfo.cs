using System;
using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static ProgramInfo ProgramInfo()
        {
            ProgramInfo programInfo = new ProgramInfo();
            programInfo.CompanyName = "Hoare Lea";
            programInfo.id = "SAM_gbXML";
            programInfo.Platform = Environment.OSVersion.ToString();
            programInfo.ProductName = "Autodesk Revit 2020 BEES";
            programInfo.Version = "2020 20191031_1115(x64)";


            return programInfo;
        }
    }
}