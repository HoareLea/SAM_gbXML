using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static IPartition ToSAM_Partition(this SpaceBoundary spaceBoundary, double tolerance = Tolerance.Distance)
        {
            if (spaceBoundary == null)
                return null;

            Polygon3D polygon3D = spaceBoundary.PlanarGeometry.ToSAM(tolerance);
            if (polygon3D == null)
                return null;

            return Analytical.Create.HostPartition(new Face3D(polygon3D));
        }

        public static IPartition ToSAM_Partition(this gbXMLSerializer.Surface surface, double tolerance = Tolerance.Distance)
        {
            Polygon3D polygon3D = surface?.PlanarGeometry?.ToSAM(tolerance);
            if (polygon3D == null)
            {
                return null;
            }

            Face3D face3D = new Face3D(polygon3D);
            if(face3D == null)
            {
                return null;
            }

            IPartition result = null;
            switch(surface.surfaceType)
            {
                case surfaceTypeEnum.Air:
                    result = new AirPartition(face3D);
                    break;

                case surfaceTypeEnum.Ceiling:
                    FloorType floorType = new FloorType(surface.Name);
                    result = Analytical.Create.HostPartition(face3D, floorType, tolerance);
                    break;
            }

            if(result is IHostPartition)
            {
                IHostPartition hostPartition = (IHostPartition)result;
                Opening[] openings_gbXML = surface.Opening;
                if (openings_gbXML != null)
                {
                    foreach (Opening opening_gbXML in openings_gbXML)
                    {
                        IOpening opening = opening_gbXML.ToSAM_Opening(tolerance);
                        if (opening != null)
                            hostPartition.AddOpening(opening);
                    }
                }
            }

            return result;
        }

    }
}
