using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.Spatial;
using System.Collections.Generic;
using System.Linq;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts gbXML PolyLoop into SAM Geometry Spatial Polygon3D
        /// </summary>
        /// <param name="polyLoop">gbXML PolyLoop</param>
        /// <param name="tolerance">Tolerance</param>
        /// <returns>SAM Geometry Spatial Polygon3D</returns>
        public static Polygon3D ToSAM(this PolyLoop polyLoop, double tolerance = Tolerance.Distance)
        {
            if (polyLoop == null)
                return null;

            CartesianPoint[] cartesianPoints = polyLoop.Points;
            if (cartesianPoints == null || cartesianPoints.Length < 3)
                return null;

            List<Point3D> point3Ds = new List<Point3D>();
            // Convert each gbXML CartesianPoint into a SAM Geometry Spatial Point3D
            foreach (CartesianPoint cartesianPoint in cartesianPoints)
            {
                Point3D point3D = cartesianPoint.ToSAM();
                if (point3D == null)
                    return null;

                point3Ds.Add(point3D);
            }

            // Calculate the origin and normal of the polygon
            Point3D origin = point3Ds.Average();
            Vector3D normal = new Vector3D();
            for (int i = 0; i < point3Ds.Count - 1; i++)
            {
                normal += (point3Ds.ElementAt(i) - origin).CrossProduct(point3Ds.ElementAt(i + 1) - origin);
            }

            // Normalize the normal vector
            normal = normal.Unit;

            // Create a SAM Geometry Spatial Polygon3D using the origin, normal, and points
            return Spatial.Create.Polygon3D(normal, point3Ds);
        }

        /// <summary>
        /// Converts gbXML PlanarGeometry into SAM Geometry Spatial Polygon3D
        /// </summary>
        /// <param name="planarGeometry">gbXML PlanarGeometry</param>
        /// <param name="tolerance">Tolerance</param>
        /// <returns>SAM Geometry Spatial Polygon3D</returns>
        public static Polygon3D ToSAM(this PlanarGeometry planarGeometry, double tolerance = Tolerance.Distance)
        {
            // Convert the PolyLoop property of the gbXML PlanarGeometry into a SAM Geometry Spatial Polygon3D
            return planarGeometry?.PolyLoop?.ToSAM(tolerance);
        }

    }
}
