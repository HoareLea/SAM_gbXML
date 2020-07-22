using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static openingTypeEnum OpeningTypeEnum(this ApertureType apertureType)
        {
            if (apertureType == Analytical.ApertureType.Undefined)
                return openingTypeEnum.Air;

            switch(apertureType)
            {
                case Analytical.ApertureType.Door:
                    return openingTypeEnum.NonSlidingDoor;
                case Analytical.ApertureType.Window:
                    return openingTypeEnum.OperableWindow;
            }

            return openingTypeEnum.Air;
        }
    }
}