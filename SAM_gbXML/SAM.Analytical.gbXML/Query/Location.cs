using gbXMLSerializer;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static Point3D Location(this ShellGeometry shellGeometry, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.Distance)
        {
            return Location(shellGeometry?.ClosedShell, silverSpacing, tolerance);
        }

        public static Point3D Location(this ClosedShell closedShell, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.Distance)
        {
            Shell shell = closedShell?.ToSAM(tolerance);
            if (shell == null)
                return null;

            return shell.InternalPoint3D(silverSpacing, tolerance);
        }
    }
}