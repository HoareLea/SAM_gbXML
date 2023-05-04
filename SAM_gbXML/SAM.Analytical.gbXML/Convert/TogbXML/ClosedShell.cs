using gbXMLSerializer;
using SAM.Geometry.gbXML;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A collection of static methods for converting various SAM Analytical objects to the gbXML format.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a collection of SAM Analytical Panels to a ClosedShell in gbXML format.
        /// </summary>
        /// <param name="panels">The collection of SAM Analytical Panels to convert.</param>
        /// <param name="tolerance">The tolerance to use for the conversion.</param>
        /// <returns>The resulting ClosedShell in gbXML format.</returns>
        public static ClosedShell TogbXML(this IEnumerable<SAM.Analytical.Panel> panels, double tolerance = Core.Tolerance.MicroDistance)
        {
            // Check if panels is null, and return null if so
            if (panels == null)
                return null;

            // Initialize a new list of PolyLoops to store converted panel faces
            List<PolyLoop> polyLoops = new List<PolyLoop>();

            // Loop through each panel in panels
            foreach (SAM.Analytical.Panel panel in panels)
            {
                // Get the face3D of the panel, convert it to a PolyLoop, and add it to polyLoops if not null
                PolyLoop polyLoop = panel?.GetFace3D(false)?.TogbXML_PolyLoop(tolerance);
                if (polyLoop != null)
                    polyLoops.Add(polyLoop);
            }

            // Initialize a new ClosedShell and set its PolyLoops property to the array representation of polyLoops
            ClosedShell closedShell = new ClosedShell();
            closedShell.PolyLoops = polyLoops.ToArray();

            // Return the resulting ClosedShell
            return closedShell;
        }

    }
}
