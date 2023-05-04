using gbXMLSerializer;
using SAM.Geometry.Spatial;

namespace SAM.Geometry.gbXML
{
    /// <summary>
    /// A static class for converting SAM Geometry Spatial objects to gbXML objects
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a SAM Geometry Spatial closed planar 3D shape to a gbXML RectangularGeometry object
        /// </summary>
        /// <param name="closedPlanar3D">The input closed planar 3D shape to be converted</param>
        /// <param name="tolerance">The tolerance to be used for the conversion (optional, default value is Core.Tolerance.MicroDistance)</param>
        /// <returns>A new RectangularGeometry object</returns>
        public static RectangularGeometry TogbXML_RectangularGeometry(this IClosedPlanar3D closedPlanar3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            // Check if the input shape is null
            if (closedPlanar3D == null)
            {
                return null;
            }

            // Convert the input shape to a temporary variable for processing
            IClosedPlanar3D closedPlanar3D_Temp = closedPlanar3D;

            // If the input shape is a Face3D, convert it to its external edge
            if (closedPlanar3D_Temp is Face3D face3D)
            {
                closedPlanar3D_Temp = face3D.GetExternalEdge3D();
            }

            // Get the plane of the shape
            Plane plane = closedPlanar3D_Temp.GetPlane();

            // Convert the shape to its 2D projection and get its bounding box
            Planar.BoundingBox2D boundingBox2D = plane.Convert(closedPlanar3D_Temp).GetBoundingBox();

            // Get the area of the original 3D shape and its 2D bounding box
            double area_closedPlanar3D = closedPlanar3D_Temp.GetArea();
            double area_boundingBox2D = boundingBox2D.GetArea();

            double width = boundingBox2D.Width;
            double height = boundingBox2D.Height;

            // If the area of the 3D shape and its bounding box are significantly different, adjust the width and height 
            // to maintain the original shape's aspect ratio
            if (System.Math.Abs(area_closedPlanar3D - area_boundingBox2D) > Core.Tolerance.MacroDistance)
            {
                //TODO: find better way to keep side ratio
                width = System.Math.Sqrt(area_closedPlanar3D);
                height = area_closedPlanar3D / width;
            }

            // Create a new RectangularGeometry object
            RectangularGeometry rectangularGeometry = new RectangularGeometry
            {
                // Set its properties
                Azimuth = Spatial.Query.Azimuth(closedPlanar3D_Temp, Vector3D.WorldY).ToString(),
                Width = width.ToString(),
                Height = height.ToString(),
                CartesianPoint = plane.Convert(boundingBox2D.Min).TogbXML(tolerance),
                Tilt = Spatial.Query.Tilt(closedPlanar3D_Temp).ToString()
            };


            // Return the new RectangularGeometry object
            return rectangularGeometry;
        }

        /// <summary>
        /// Converts a Face3DObject to a gbXML RectangularGeometry object.
        /// </summary>
        /// <param name="face3DObject">The input Face3DObject to convert.</param>
        /// <param name="tolerance">The tolerance to use for the conversion. Default is Core.Tolerance.MicroDistance.</param>
        /// <returns>A RectangularGeometry object.</returns>
        public static RectangularGeometry TogbXML_RectangularGeometry(this IFace3DObject face3DObject, double tolerance = Core.Tolerance.MicroDistance)
        {
            // Call the TogbXML_RectangularGeometry method with the Face3D of the Face3DObject as input
            return TogbXML_RectangularGeometry(face3DObject?.Face3D, tolerance);
        }
    }
}
