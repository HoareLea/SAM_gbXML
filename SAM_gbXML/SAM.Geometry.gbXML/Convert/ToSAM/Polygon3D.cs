using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.Spatial;
using System.Collections.Generic;
using System.Linq;

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
                Point3D point3D = cartesianPoint.ToSAM();
                if (point3D == null)
                    return null;

                point3Ds.Add(point3D);
            }

            Point3D origin = point3Ds.Average();
            Vector3D normal = new Vector3D();
            for (int i = 0; i < point3Ds.Count - 1; i++)
            {
                normal += (point3Ds.ElementAt(i) - origin).CrossProduct(point3Ds.ElementAt(i + 1) - origin);
            }

            normal = normal.Unit;

            return Spatial.Create.Polygon3D(normal, point3Ds);
        }

        public static Polygon3D ToSAM(this PlanarGeometry planarGeometry, double tolerance = Tolerance.Distance)
        {
            return planarGeometry?.PolyLoop?.ToSAM(tolerance);
        }

    }
}
