using gbXMLSerializer;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        public static PlanarGeometry TogbXML(this Spatial.IClosedPlanar3D closedPlanar3D, Spatial.Vector3D normal = null, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (closedPlanar3D == null)
                return null;

            PlanarGeometry planarGeometry = new PlanarGeometry();
            planarGeometry.PolyLoop = closedPlanar3D.TogbXML_PolyLoop(normal, tolerance);

            return planarGeometry;
        }

    }
}
