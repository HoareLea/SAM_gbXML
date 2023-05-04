using gbXMLSerializer;

namespace SAM.Geometry.gbXML
{
    /// <summary>
    /// Provides methods for converting <see cref="Spatial.IClosedPlanar3D"/> objects to gbXML format.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts the <see cref="Spatial.IClosedPlanar3D"/> object to a <see cref="PlanarGeometry"/> object in gbXML format.
        /// </summary>
        /// <param name="closedPlanar3D">The <see cref="Spatial.IClosedPlanar3D"/> object to convert.</param>
        /// <param name="tolerance">The tolerance to use for the conversion (optional, default is <see cref="Core.Tolerance.MicroDistance"/>).</param>
        /// <returns>A <see cref="PlanarGeometry"/> object in gbXML format.</returns>
        public static PlanarGeometry TogbXML(this Spatial.IClosedPlanar3D closedPlanar3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (closedPlanar3D == null)
                return null;

            PlanarGeometry planarGeometry = new PlanarGeometry();
            planarGeometry.PolyLoop = closedPlanar3D.TogbXML_PolyLoop(tolerance);

            return planarGeometry;
        }

        /// <summary>
        /// Converts the <see cref="Spatial.IFace3DObject"/> object to a <see cref="PlanarGeometry"/> object in gbXML format.
        /// </summary>
        /// <param name="face3DObject">The <see cref="Spatial.IFace3DObject"/> object to convert.</param>
        /// <param name="tolerance">The tolerance to use for the conversion (optional, default is <see cref="Core.Tolerance.MicroDistance"/>).</param>
        /// <returns>A <see cref="PlanarGeometry"/> object in gbXML format.</returns>
        public static PlanarGeometry TogbXML_PlanarGeometry(this Spatial.IFace3DObject face3DObject, double tolerance = Core.Tolerance.MicroDistance)
        {
            return TogbXML(face3DObject?.Face3D, tolerance);
        }

    }
}
