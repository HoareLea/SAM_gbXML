using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    // This class provides extension methods for converting objects to gbXML format
    public static partial class Convert
    {
        /// <summary>
        /// Converts a space within a building model to a gbXML shell geometry
        /// </summary>
        /// <param name="buildingModel">The building model containing the space</param>
        /// <param name="space">The space to convert</param>
        /// <param name="tolerance">The tolerance to use when converting geometry to gbXML format (optional, defaults to Core.Tolerance.MicroDistance)</param>
        /// <returns>A gbXML shell geometry representing the space, or null if conversion failed</returns>
        public static ShellGeometry TogbXML(this BuildingModel buildingModel, Space space, double tolerance = Core.Tolerance.MicroDistance)
        {
            // Get the shell geometry for the specified space
            Geometry.Spatial.Shell shell = buildingModel?.GetShell(space);

            // If shell is null, conversion failed, so return null
            if (shell == null)
            {
                return null;
            }

            // Create a new ShellGeometry object and set its properties
            ShellGeometry result = new ShellGeometry()
            {
                id = Core.gbXML.Query.Id(space, typeof(ShellGeometry)),
                unit = lengthUnitEnum.Meters,
                ClosedShell = Geometry.gbXML.Convert.TogbXML(shell, tolerance)

            };

            // Return the resulting ShellGeometry object
            return result;
        }
    }
}
