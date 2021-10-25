using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static ShellGeometry TogbXML(this ArchitecturalModel architecturalModel, Space space, double tolerance = Core.Tolerance.MicroDistance)
        {
            Geometry.Spatial.Shell shell = architecturalModel?.GetShell(space);
            if(shell == null)
            {
                return null;
            }

            ShellGeometry result = new ShellGeometry()
            {
                id = Core.gbXML.Query.Id(space, typeof(ShellGeometry)),
                unit = lengthUnitEnum.Meters,
                ClosedShell = Geometry.gbXML.Convert.TogbXML(shell, tolerance)

            };

            return result;
        }

    }
}
