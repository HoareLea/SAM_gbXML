using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A static class for converting gbXML objects to SAM objects.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a gbXML opening to a SAM opening.
        /// </summary>
        /// <param name="opening">The gbXML opening to convert.</param>
        /// <param name="tolerance">The tolerance to use for converting geometries.</param>
        /// <returns>The converted SAM opening.</returns>
        public static IOpening ToSAM_Opening(this gbXMLSerializer.Opening opening, double tolerance = Tolerance.MicroDistance)
        {
            // Convert the gbXML polygon to a SAM Polygon3D.
            Polygon3D polygon3D = opening?.pg?.ToSAM(tolerance);

            // If the polygon is null, return null.
            if (polygon3D == null)
            {
                return null;
            }

            OpeningType openingType = null;
            switch (opening.openingType)
            {
                // If the opening is a non-sliding or sliding door, create a DoorType.
                case gbXMLSerializer.openingTypeEnum.NonSlidingDoor:
                case gbXMLSerializer.openingTypeEnum.SlidingDoor:
                    openingType = new DoorType(opening.Name);
                    break;

                // Otherwise, create a WindowType.
                default:
                    openingType = new WindowType(opening.Name);
                    break;
            }

            // If the opening type is null, return null.
            if (openingType == null)
            {
                return null;
            }

            // Create the SAM opening using the converted Polygon3D and OpeningType.
            IOpening result = Analytical.Create.Opening(System.Guid.NewGuid(), openingType, new Face3D(polygon3D), Analytical.Query.OpeningLocation(polygon3D, tolerance));
            return result;
        }

    }
}
