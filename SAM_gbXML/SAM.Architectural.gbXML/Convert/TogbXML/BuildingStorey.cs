using gbXMLSerializer;
using SAM.Architectural;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static BuildingStorey TogbXML(this Level level, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (level == null)
                return null;

            BuildingStorey buildingStorey = new BuildingStorey();
            buildingStorey.id = level.Guid.ToString();
            buildingStorey.Level = Core.Query.Round(level.Elevation, tolerance).ToString();
            buildingStorey.Name = level.Name;
            
            return buildingStorey;
        }

    }
}
