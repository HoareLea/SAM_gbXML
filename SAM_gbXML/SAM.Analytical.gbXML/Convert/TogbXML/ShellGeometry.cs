using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static ShellGeometry TogbXML(this IEnumerable<Panel> panels, Space space, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (panels == null || space == null)
                return null;

            ShellGeometry shellGeometry = new ShellGeometry();
            shellGeometry.id = Core.gbXML.Query.Id(space, typeof(ShellGeometry)); ;
            shellGeometry.unit = lengthUnitEnum.Meters;
            shellGeometry.ClosedShell = panels.TogbXML(tolerance);

            return shellGeometry;
        }

    }
}
