using gbXMLSerializer;
using SAM.Geometry.Spatial;
using System.Collections.Generic;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        public static ClosedShell TogbXML(this Shell shell, double tolerance = Core.Tolerance.MicroDistance)
        {
            List<Face3D> face3Ds = shell?.Face3Ds;
            if (face3Ds == null || face3Ds.Count == 0)
                return null;

            List<PolyLoop> polyLoops = new List<PolyLoop>();
            foreach (Face3D face3D in face3Ds)
            {
                PolyLoop polyLoop = face3D?.TogbXML_PolyLoop(tolerance);
                if (polyLoop != null)
                    polyLoops.Add(polyLoop);
            }

            ClosedShell closedShell = new ClosedShell()
            {
                PolyLoops = polyLoops.ToArray()
            };

            return closedShell;
        }

    }
}
