using System;
using gbXMLSerializer;

namespace SAM.Core.gbXML
{
    public static partial class Query
    {
        public static PersonInfo PersonInfo()
        {
            PersonInfo personInfo = new PersonInfo();
            personInfo.id = Environment.UserName;

            return personInfo;
        }
    }
}