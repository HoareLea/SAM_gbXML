using System;
using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    // This class contains a method for querying gbXML files
    public static partial class Query
    {
        /// <summary>
        /// Creates a ModifiedBy object that contains information about the user and program that modified a gbXML file
        /// </summary>
        /// <returns>A ModifiedBy object</returns>
        public static ModifiedBy ModifiedBy()
        {
            // Create a new ModifiedBy object
            ModifiedBy modifiedBy = new ModifiedBy();

            // Set the ModifiedBy object's date property to the current date and time
            modifiedBy.date = DateTime.Now;

            // Set the ModifiedBy object's personId property to the user name of the current environment
            modifiedBy.personId = Environment.UserName;

            // Set the ModifiedBy object's programId property to "SAM_gbXML"
            modifiedBy.programId = "SAM_gbXML";

            // Return the ModifiedBy object
            return modifiedBy;
        }
    }
}
