using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static ApertureType ApertureType(this openingTypeEnum openingTypeEnum)
        {
            switch(openingTypeEnum)
            {
                case openingTypeEnum.Air:
                    return Analytical.ApertureType.Undefined;
                case openingTypeEnum.FixedSkylight:
                case openingTypeEnum.FixedWindow:
                case openingTypeEnum.OperableSkylight:
                case openingTypeEnum.OperableWindow:
                    return Analytical.ApertureType.Window;
                case openingTypeEnum.NonSlidingDoor:
                case openingTypeEnum.SlidingDoor:
                    return Analytical.ApertureType.Door;
            }

            return Analytical.ApertureType.Undefined;
        }
    }
}