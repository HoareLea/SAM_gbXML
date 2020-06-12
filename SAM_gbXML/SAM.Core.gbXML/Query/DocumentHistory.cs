using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static DocumentHistory DocumentHistory()
        {
            DocumentHistory documentHistory = new DocumentHistory();
            documentHistory.ProgramInfo = ProgramInfo();

            return documentHistory;
        }
    }
}