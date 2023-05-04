using gbXMLSerializer;
using SAM.Geometry.gbXML;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Static class containing extension methods for converting Analytical classes to gbXML classes
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a <see cref="PlanarBoundary3D"/> to a <see cref="PlanarGeometry"/> object for gbXML serialization
        /// </summary>
        /// <param name="planarBoundary3D">Planar boundary object to convert</param>
        /// <param name="tolerance">Tolerance used in conversion</param>
        /// <returns>Returns a <see cref="PlanarGeometry"/> object if successful, null otherwise</returns>
        public static PlanarGeometry TogbXML(this PlanarBoundary3D planarBoundary3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (planarBoundary3D == null)
                return null;

            // Convert PlanarBoundary3D to Face3D
            Geometry.Spatial.Face3D face3D = planarBoundary3D.GetFace3D();
            if (face3D == null)
                return null;

            // Convert Face3D to PlanarGeometry
            PlanarGeometry planarGeometry = new PlanarGeometry();
            planarGeometry.PolyLoop = face3D.TogbXML_PolyLoop(tolerance);

            return planarGeometry;
        }

    }
}
