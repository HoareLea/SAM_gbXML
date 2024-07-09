using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a collection of panels to a shell geometry object in gbXML format.
        /// </summary>
        /// <param name="panels">The collection of panels to convert.</param>
        /// <param name="space">The space object that the shell geometry belongs to.</param>
        /// <param name="tolerance">The tolerance to use for the conversion.</param>
        /// <returns>The converted shell geometry object.</returns>
        public static ShellGeometry TogbXML(this IEnumerable<IPanel> panels, ISpace space, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (panels == null || space == null)
                return null;

            // Create a new shell geometry object and set its properties
            ShellGeometry shellGeometry = new ShellGeometry();
            shellGeometry.id = Core.gbXML.Query.Id(space, typeof(ShellGeometry));
            shellGeometry.unit = lengthUnitEnum.Meters;

            // Convert the panels to a closed shell and assign it to the shell geometry object
            shellGeometry.ClosedShell = panels.TogbXML(tolerance);

            return shellGeometry;
        }
    }
}
