using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        /// <summary>
        /// Returns the opening type enum based on the aperture type.
        /// </summary>
        /// <param name="apertureType">The ApertureType to convert.</param>
        /// <returns>The corresponding opening type.</returns>
        public static openingTypeEnum OpeningTypeEnum(this ApertureType apertureType)
        {
            // Check if the ApertureType is undefined, return Air as the default opening type.
            if (apertureType == Analytical.ApertureType.Undefined)
                return openingTypeEnum.Air;

            // Map ApertureType to the corresponding opening type.
            switch (apertureType)
            {
                case Analytical.ApertureType.Door:
                    return openingTypeEnum.NonSlidingDoor;
                case Analytical.ApertureType.Window:
                    return openingTypeEnum.OperableWindow;
            }

            // Return Air as the default opening type if the ApertureType is not Door or Window.
            return openingTypeEnum.Air;
        }
    }
}
