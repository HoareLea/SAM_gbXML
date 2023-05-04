using System;
using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        /// <summary>
        /// Generates a CreatedBy object with the provided CAD model ID, current date and time, user name and program ID.
        /// </summary>
        /// <param name="cADModelId">The CAD model ID as a GUID.</param>
        /// <returns>The CreatedBy object with the specified CAD model ID and other information.</returns>
        public static CreatedBy CreatedBy(this Guid cADModelId)
        {
            CreatedBy createdBy = new CreatedBy();
            createdBy.CADModelId = cADModelId.ToString();
            createdBy.date = DateTime.Now;
            createdBy.personId = Environment.UserName;
            createdBy.programId = "SAM_gbXML";

            return createdBy;
        }
    }
}
