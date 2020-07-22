using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.Spatial;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        public static Point3D ToSAM(this CartesianPoint cartesianPoint, double tolerance = Tolerance.Distance)
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

            return new Point3D(Core.Query.Round(x), Core.Query.Round(y), Core.Query.Round(z));
        }

    }
}
