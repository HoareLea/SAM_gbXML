using gbXMLSerializer; 
using SAM.Geometry.gbXML; 
using SAM.Geometry.Spatial; 

namespace SAM.Analytical.gbXML 
{
    public static partial class Query 
    {
        /// <summary>
        /// Returns the location of a shell geometry as a Point3D object
        /// </summary>
        /// <param name="shellGeometry">The shell geometry to get the location of</param>
        /// <param name="silverSpacing">The distance between silver spaces</param>
        /// <param name="tolerance">The tolerance value to use</param>
        /// <returns>The location of the shell geometry as a Point3D object</returns>
        public static Point3D Location(this ShellGeometry shellGeometry, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.Distance)
        {
            return Location(shellGeometry?.ClosedShell, silverSpacing, tolerance);
            // Returns the location of the closed shell of the given shell geometry, using the provided parameters
        }

        /// <summary>
        /// Returns the location of a closed shell as a Point3D object
        /// </summary>
        /// <param name="closedShell">The closed shell to get the location of</param>
        /// <param name="silverSpacing">The distance between silver spaces</param>
        /// <param name="tolerance">The tolerance value to use</param>
        /// <returns>The location of the closed shell as a Point3D object</returns>
        public static Point3D Location(this ClosedShell closedShell, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.Distance)
        {
            Shell shell = closedShell?.ToSAM(tolerance);
            // Converts the given closed shell to a SAM shell, using the provided tolerance value
            if (shell == null)
                return null; // If the conversion fails, returns null

            return shell.InternalPoint3D(silverSpacing, tolerance);
            // Returns the internal point of the SAM shell, using the provided parameters
        }
    }
}
