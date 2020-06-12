using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static DocumentHistory DocumentHistory(System.Guid cADModelId)
        {
            DocumentHistory documentHistory = new DocumentHistory();
            documentHistory.ProgramInfo = ProgramInfo();
            documentHistory.CreatedBy = CreatedBy(cADModelId);
            documentHistory.ModifiedBy = ModifiedBy();
            documentHistory.PersonInfo = PersonInfo();

            return documentHistory;
        }
    }
}