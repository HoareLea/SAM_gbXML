// Import the gbXMLSerializer namespace
using gbXMLSerializer;

// Create a namespace for the Query class
namespace SAM.Analytical.gbXML
{
    // Declare a static partial class for the Query class
    public static partial class Query
    {
        /// <summary>
        /// Converts an openingTypeEnum value to an ApertureType value.
        /// </summary>
        /// <param name="openingTypeEnum">The openingTypeEnum value to convert.</param>
        /// <returns>The corresponding ApertureType value.</returns>
        public static ApertureType ApertureType(this openingTypeEnum openingTypeEnum)
        {
            // Use a switch statement to determine the corresponding ApertureType
            switch (openingTypeEnum)
            {
                // If the openingTypeEnum is Air, return Undefined
                case openingTypeEnum.Air:
                    return Analytical.ApertureType.Undefined;

                // If the openingTypeEnum is FixedSkylight, FixedWindow, OperableSkylight, or OperableWindow, return Window
                case openingTypeEnum.FixedSkylight:
                case openingTypeEnum.FixedWindow:
                case openingTypeEnum.OperableSkylight:
                case openingTypeEnum.OperableWindow:
                    return Analytical.ApertureType.Window;

                // If the openingTypeEnum is NonSlidingDoor or SlidingDoor, return Door
                case openingTypeEnum.NonSlidingDoor:
                case openingTypeEnum.SlidingDoor:
                    return Analytical.ApertureType.Door;
            }

            // If the openingTypeEnum is not one of the specified values, return Undefined
            return Analytical.ApertureType.Undefined;
        }
    }
}
