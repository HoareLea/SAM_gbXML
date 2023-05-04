using gbXMLSerializer;
using SAM.Geometry.Spatial;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a Point3D to a CartesianPoint with coordinates rounded to the specified tolerance.
        /// </summary>
        /// <param name="point3D">The Point3D to convert.</param>
        /// <param name="tolerance">The tolerance used to round the coordinates. Default value is Core.Tolerance.MicroDistance.</param>
        /// <returns>The converted CartesianPoint.</returns>
        public static CartesianPoint TogbXML(this Point3D point3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (point3D == null)
                return null;

            CartesianPoint cartesianPoint = new CartesianPoint();
            cartesianPoint.Coordinate = new string[3];
            cartesianPoint.Coordinate[0] = Core.Query.Round(point3D.X, tolerance).ToString();
            cartesianPoint.Coordinate[1] = Core.Query.Round(point3D.Y, tolerance).ToString();
            cartesianPoint.Coordinate[2] = Core.Query.Round(point3D.Z, tolerance).ToString();

            return cartesianPoint;
        }
    }
}
