using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static ShellGeometry TogbXML(this IEnumerable<Panel> panels, string id, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (panels == null || string.IsNullOrWhiteSpace(id))
                return null;

            ShellGeometry shellGeometry = new ShellGeometry();
            shellGeometry.id = id;
            shellGeometry.unit = lengthUnitEnum.Meters;
            shellGeometry.ClosedShell = panels.TogbXML(tolerance);

            return shellGeometry;
        }

    }
}
