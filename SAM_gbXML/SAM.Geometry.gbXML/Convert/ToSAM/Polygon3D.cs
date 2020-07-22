using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.Spatial;
using System.Collections.Generic;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        public static Polygon3D ToSAM(this PolyLoop polyLoop, double tolerance = Tolerance.Distance)
        {
            if (polyLoop == null)
                return null;

            CartesianPoint[] cartesianPoints = polyLoop.Points;
            if (cartesianPoints == null || cartesianPoints.Length < 3)
                return null;

            List<Point3D> point3Ds = new List<Point3D>();
            foreach (CartesianPoint cartesianPoint in cartesianPoints)
            {
                Point3D point3D = cartesianPoint.ToSAM(tolerance);
                if (point3D == null)
                    return null;

                point3Ds.Add(point3D);
            }

            return new Polygon3D(point3Ds, tolerance);
        }

        public static Polygon3D ToSAM(this PlanarGeometry planarGeometry, double tolerance = Tolerance.Distance)
        {
            return planarGeometry?.PolyLoop?.ToSAM(tolerance);
        }

    }
}
