using gbXMLSerializer;
using SAM.Geometry.Spatial;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        public static RectangularGeometry TogbXML_RectangularGeometry(this IClosedPlanar3D closedPlanar3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (closedPlanar3D == null)
                return null;

            Plane plane = closedPlanar3D.GetPlane();
            Planar.BoundingBox2D boundingBox2D = plane.Convert(closedPlanar3D).GetBoundingBox();

            RectangularGeometry rectangularGeometry = new RectangularGeometry();
            rectangularGeometry.Azimuth = Query.Azimuth(closedPlanar3D, Vector3D.WorldY).ToString();
            rectangularGeometry.Width = boundingBox2D.Width.ToString();
            rectangularGeometry.Height = boundingBox2D.Height.ToString();
            rectangularGeometry.CartesianPoint = plane.Convert(boundingBox2D.Min).TogbXML(tolerance);
            rectangularGeometry.Tilt = Query.Tilt(closedPlanar3D).ToString();
            return rectangularGeometry;
        }

    }
}
