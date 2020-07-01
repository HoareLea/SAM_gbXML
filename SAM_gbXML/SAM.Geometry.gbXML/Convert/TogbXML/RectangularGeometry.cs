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
            double area_closedPlanar3D = closedPlanar3D.GetArea();
            double area_boundingBox2D = boundingBox2D.GetArea();

            double width = boundingBox2D.Width;
            double height = boundingBox2D.Height;
            if(System.Math.Abs(area_closedPlanar3D - area_boundingBox2D) > Core.Tolerance.MacroDistance)
            {
                //TODO: find better way to keep side ratio
                width = System.Math.Sqrt(area_closedPlanar3D);
                height = area_closedPlanar3D / width;
            }

            RectangularGeometry rectangularGeometry = new RectangularGeometry();
            rectangularGeometry.Azimuth = Spatial.Query.Azimuth(closedPlanar3D, Vector3D.WorldY()).ToString();
            rectangularGeometry.Width = width.ToString();
            rectangularGeometry.Height = height.ToString();
            rectangularGeometry.CartesianPoint = plane.Convert(boundingBox2D.Min).TogbXML(tolerance);
            rectangularGeometry.Tilt = Spatial.Query.Tilt(closedPlanar3D).ToString();
            return rectangularGeometry;
        }

    }
}
