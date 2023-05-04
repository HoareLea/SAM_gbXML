using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        /// <summary>
        /// Returns a <see cref="DocumentHistory"/> object containing information about the creation and modification of a gbXML document.
        /// </summary>
        /// <param name="cADModelId">The GUID of the gbXML document.</param>
        /// <returns>A <see cref="DocumentHistory"/> object containing document history information.</returns>
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
