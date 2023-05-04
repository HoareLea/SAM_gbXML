using gbXMLSerializer;
using SAM.Geometry.Spatial;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts an <see cref="IFace3DObject"/> to a gbXML <see cref="SpaceBoundary"/>.
        /// </summary>
        /// <param name="face3DObject">An <see cref="IFace3DObject"/> to convert.</param>
        /// <param name="tolerance">Tolerance for the conversion.</param>
        /// <returns>A gbXML <see cref="SpaceBoundary"/> representing the input <see cref="IFace3DObject"/>.</returns>
        public static SpaceBoundary TogbXML_SpaceBoundary(this IFace3DObject face3DObject, double tolerance = Core.Tolerance.MicroDistance)
        {
            // Check if the input object is null or if it does not contain a valid SAM Geometry Spatial Face3D object
            Face3D face3D = face3DObject?.Face3D;
            if (face3D == null)
                return null;

            // Create a new gbXML SpaceBoundary object and set its surfaceIdRef and PlanarGeometry properties
            SpaceBoundary spaceBoundary = new SpaceBoundary()
            {
                surfaceIdRef = Core.gbXML.Query.Id(face3DObject, typeof(gbXMLSerializer.Surface)),
                PlanarGeometry = face3D.TogbXML(tolerance)
            };

            return spaceBoundary;
        }
    }
}
