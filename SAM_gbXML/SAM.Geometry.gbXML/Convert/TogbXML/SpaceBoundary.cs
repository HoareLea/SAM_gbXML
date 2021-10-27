using gbXMLSerializer;
using SAM.Geometry.Spatial;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        public static SpaceBoundary TogbXML_SpaceBoundary(this IFace3DObject face3DObject, double tolerance = Core.Tolerance.MicroDistance)
        {
            Face3D face3D = face3DObject?.Face3D;
            if (face3D == null)
                return null;

            SpaceBoundary spaceBoundary = new SpaceBoundary()
            {
                surfaceIdRef = Core.gbXML.Query.Id(face3DObject, typeof(gbXMLSerializer.Surface)),
                PlanarGeometry = face3D.TogbXML(tolerance)
            };

            return spaceBoundary;
        }
    }
}
