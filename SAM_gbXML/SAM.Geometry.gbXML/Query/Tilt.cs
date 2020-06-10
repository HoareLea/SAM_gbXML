using SAM.Geometry.Spatial;
using System;

namespace SAM.Geometry.gbXML
{
    public static partial class Query
    {
        public static double Tilt(this IClosedPlanar3D closedPlanar3D)
        {
            if (closedPlanar3D == null)
                return double.NaN;

            Vector3D normal = closedPlanar3D.GetPlane()?.Normal;
            if (normal == null)
                return double.NaN;

            if (!Spatial.Query.Clockwise(closedPlanar3D))
                normal.Negate();

            return normal.Angle(Plane.WorldXY.Normal) * (180 / Math.PI);
        }
    }
}