using gbXMLSerializer;
using SAM.Geometry.Spatial;

namespace SAM.Geometry.gbXML
{
    /// <summary>
    /// Provides conversion methods between gbXML geometry objects and SAM geometry objects
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a <see cref="CartesianPoint"/> to a <see cref="SAM.Geometry.Spatial.Point3D"/>.
        /// </summary>
        /// <param name="cartesianPoint">The Cartesian point to convert.</param>
        /// <returns>A <see cref="Point3D"/> if the conversion was successful, otherwise null.</returns>
        public static Point3D ToSAM(this CartesianPoint cartesianPoint)
        {
            if (cartesianPoint == null)
                return null;

            double x = double.NaN;
            if (!double.TryParse(cartesianPoint.Coordinate[0], out x))
                return null;

            double y = double.NaN;
            if (!double.TryParse(cartesianPoint.Coordinate[1], out y))
                return null;

            double z = double.NaN;
            if (!double.TryParse(cartesianPoint.Coordinate[2], out z))
                return null;

            return new Point3D(x, y, z);
        }
    }
}
