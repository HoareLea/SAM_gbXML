using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static ShellGeometry TogbXML(this BuildingModel buildingModel, Space space, double tolerance = Core.Tolerance.MicroDistance)
        {
            Geometry.Spatial.Shell shell = buildingModel?.GetShell(space);
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
