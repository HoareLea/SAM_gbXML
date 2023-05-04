using System;
using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    /// <summary>
    /// A static class that contains methods for querying gbXML data.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Returns a new instance of the PersonInfo class with the id property set to the current user's username.
        /// </summary>
        /// <returns>A new instance of the PersonInfo class with the id property set to the current user's username.</returns>
        public static PersonInfo PersonInfo()
        {
            // create a new instance of the PersonInfo class
            PersonInfo personInfo = new PersonInfo();

            // set the id property to the current user's username
            personInfo.id = Environment.UserName;

            // return the new PersonInfo instance
            return personInfo;
        }
    }
}
