// Import the gbXMLSerializer namespace
using gbXMLSerializer;

// Define the namespace for the SAM.Analytical.gbXML Query class
namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A static partial class for querying gbXML data in the SAM.Analytical library.
    /// </summary>
    public static partial class Query
    {
/// <summary>
        /// Gets the opening type of an IOpening object using the gbXML standard.
        /// </summary>
        /// <param name="opening">The IOpening object to query.</param>
        /// <returns>An openingTypeEnum value or null if the opening is null.</returns>
        public static openingTypeEnum? OpeningTypeEnum(this IOpening opening)
        {
            // Return null if the opening is null
            if (opening == null)
            {
                return null;
            }

            // Get the analytical type of the opening using the gbXML standard
            OpeningAnalyticalType openingAnalyticalType = opening.OpeningAnalyticalType();

            //// Map the analytical type to an openingTypeEnum value using a switch statement
            //switch (openingAnalyticalType)
            //{
            //    case OpeningAnalyticalType.Door:
            //        return openingTypeEnum.NonSlidingDoor;
            //    case OpeningAnalyticalType.Window:
            //        return openingTypeEnum.OperableWindow;
            //}

            //// Return openingTypeEnum.Air if the opening is not a door or window
            //return openingTypeEnum.Air;

            // Map the analytical type to an openingTypeEnum value using a switch expression
            return openingAnalyticalType switch
            {
                OpeningAnalyticalType.Door => openingTypeEnum.NonSlidingDoor,
                OpeningAnalyticalType.Window => openingTypeEnum.OperableWindow,
                _ => openingTypeEnum.Air
            };
        }
    }
}
