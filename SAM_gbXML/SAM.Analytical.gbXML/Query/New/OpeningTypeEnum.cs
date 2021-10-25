using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static openingTypeEnum? OpeningTypeEnum(this IOpening opening)
        {
            if(opening == null)
            {
                return null;
            }

            OpeningAnalyticalType openingAnalyticalType = opening.OpeningAnalyticalType();
            switch(openingAnalyticalType)
            {
                case OpeningAnalyticalType.Door:
                    return openingTypeEnum. NonSlidingDoor;
                case OpeningAnalyticalType.Window:
                    return openingTypeEnum.OperableWindow;
            }

            return openingTypeEnum.Air;
        }
    }
}