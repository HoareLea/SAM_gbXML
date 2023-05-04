using gbXMLSerializer;
using SAM.Geometry.Spatial;
using System.Collections.Generic;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a <see cref="Shell"/> object to a <see cref="ClosedShell"/> object in gbXML format.
        /// </summary>
        /// <param name="shell">The input <see cref="Shell"/> object to be converted.</param>
        /// <param name="tolerance">The tolerance for comparison. The default value is <see cref="Core.Tolerance.MicroDistance"/>.</param>
        /// <returns>A <see cref="ClosedShell"/> object in gbXML format.</returns>
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
