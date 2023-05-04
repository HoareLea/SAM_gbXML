using gbXMLSerializer;
using SAM.Geometry.gbXML;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a PlanarBoundary3D object to a gbXML RectangularGeometry object.
        /// </summary>
        /// <param name="planarBoundary3D">The PlanarBoundary3D object to be converted.</param>
        /// <param name="tolerance">The tolerance used for the conversion. Default value is set to Core.Tolerance.MicroDistance.</param>
        /// <returns>A gbXML RectangularGeometry object that represents the converted PlanarBoundary3D object.</returns>
        public static RectangularGeometry TogbXML_RectangularGeometry(this PlanarBoundary3D planarBoundary3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (planarBoundary3D == null)
                return null;

            // Calls the TogbXML_RectangularGeometry method from SAM.Geometry.gbXML.Convert class
            // This method converts a Face3D object to a gbXML RectangularGeometry object
            // The PlanarBoundary3D object is first converted to a Face3D object
            return Geometry.gbXML.Convert.TogbXML_RectangularGeometry(planarBoundary3D.GetFace3D(), tolerance);
        }

    }
}
