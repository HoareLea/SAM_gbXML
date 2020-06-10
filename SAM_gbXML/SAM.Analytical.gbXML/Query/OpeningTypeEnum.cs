using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static openingTypeEnum OpeningTypeEnum(this ApertureType apertureType)
        {
            if (apertureType == ApertureType.Undefined)
                return openingTypeEnum.Air;

            switch(apertureType)
            {
                case ApertureType.Door:
                    return openingTypeEnum.NonSlidingDoor;
                case ApertureType.Window:
                    return openingTypeEnum.OperableWindow;
            }

            return openingTypeEnum.Air;
        }
    }
}