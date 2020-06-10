using gbXMLSerializer;
using SAM.Geometry.gbXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static PlanarGeometry TogbXML(this PlanarBoundary3D planarBoundary3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (planarBoundary3D == null)
                return null;

            Geometry.Spatial.Face3D face3D = planarBoundary3D.GetFace3D();
            if (face3D == null)
                return null;

            PlanarGeometry planarGeometry = new PlanarGeometry();
            planarGeometry.PolyLoop = face3D.TogbXML_PolyLoop(tolerance);
            
            return planarGeometry;
        }

    }
}
