using gbXMLSerializer;
using System;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static DocumentHistory DocumentHistory(this AnalyticalModel analyticalModel)
        {
            if (analyticalModel == null)
                return null;

            DocumentHistory documentHistory = new DocumentHistory();
            documentHistory.id = Core.gbXML.Query.Id(analyticalModel, typeof(DocumentHistory));
            documentHistory.ProgramInfo = Core.gbXML.Query.ProgramInfo();

            return documentHistory;
        }
    }
}