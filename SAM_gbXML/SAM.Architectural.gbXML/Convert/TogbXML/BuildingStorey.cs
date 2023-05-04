using gbXMLSerializer;
using SAM.Architectural;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Convert methods between SAM analytical objects and gbXML objects
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a SAM Level object to a gbXML BuildingStorey object
        /// </summary>
        /// <param name="level">SAM Architectural Level object to convert</param>
        /// <param name="tolerance">Tolerance for rounding the elevation value</param>
        /// <returns>gbXML BuildingStorey object converted from SAM Level object</returns>
        public static BuildingStorey TogbXML(this Level level, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (level == null)
                return null;

            BuildingStorey buildingStorey = new BuildingStorey();
            buildingStorey.id = Core.gbXML.Query.Id(level, typeof(BuildingStorey));
            buildingStorey.Level = Core.Query.Round(level.Elevation, tolerance).ToString();
            buildingStorey.Name = level.Name;

            return buildingStorey;
        }
    }
}
