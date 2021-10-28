using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static IOpening ToSAM_Opening(this gbXMLSerializer.Opening opening, double tolerance = Tolerance.MicroDistance)
        {
            Polygon3D polygon3D = opening?.pg?.ToSAM(tolerance);
            if (polygon3D == null)
            {
                return null;
            }

            OpeningType openingType = null;
            switch(opening.openingType)
            {
                case gbXMLSerializer.openingTypeEnum.NonSlidingDoor:
                case gbXMLSerializer.openingTypeEnum.SlidingDoor:
                    openingType = new DoorType(opening.Name);
                    break;

                default:
                    openingType = new WindowType(opening.Name);
                    break;
            }

            if(openingType == null)
            {
                return null;
            }

            IOpening result = Analytical.Create.Opening(System.Guid.NewGuid(), openingType, new Face3D(polygon3D), Analytical.Query.OpeningLocation(polygon3D, tolerance));
            return result;
        }

    }
}
