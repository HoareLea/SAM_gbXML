using gbXMLSerializer;
using SAM.Geometry.gbXML;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static ClosedShell TogbXML(this IEnumerable<Panel> panels, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (panels == null)
                return null;

            List<PolyLoop> polyLoops = new List<PolyLoop>();
            foreach(Panel panel in panels)
            {
                PolyLoop polyLoop = panel?.GetFace3D()?.TogbXML_PolyLoop();
                if (polyLoop != null)
                    polyLoops.Add(polyLoop);
            }

            ClosedShell closedShell = new ClosedShell();
            closedShell.PolyLoops = polyLoops.ToArray();

            return closedShell;
        }

    }
}
