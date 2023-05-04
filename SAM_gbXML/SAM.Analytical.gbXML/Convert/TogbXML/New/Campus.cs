using gbXMLSerializer;
using System.Collections.Generic;
using System.Linq;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Class for converting BuildingModel to gbXML Campus object.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a BuildingModel object to a gbXML Campus object.
        /// </summary>
        /// <param name="buildingModel">The BuildingModel to be converted.</param>
        /// <param name="silverSpacing">The spacing for silver geometry (default value is Core.Tolerance.MacroDistance).</param>
        /// <param name="tolerance_Angle">The tolerance for angle (default value is Core.Tolerance.Angle).</param>
        /// <param name="tolerance_Distance">The tolerance for distance (default value is Core.Tolerance.Distance).</param>
        /// <returns>A gbXML Campus object.</returns>
        public static Campus TogbXML_Campus(this BuildingModel buildingModel, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.Distance)
        {
            if (buildingModel == null)
                return null;

            BuildingModel buildingModel_Temp = new BuildingModel(buildingModel);

            Campus campus = new Campus();
            campus.id = Core.gbXML.Query.Id(buildingModel_Temp, typeof(Campus));
            campus.Location = Core.gbXML.Convert.TogbXML(buildingModel_Temp.Location, buildingModel_Temp.Address, tolerance_Distance);

            buildingModel_Temp.SplitByInternalEdges(tolerance_Distance);
            buildingModel_Temp.OrientPartitions(true, true, silverSpacing, tolerance_Distance);
            buildingModel_Temp.FixEdges(tolerance_Distance);

            campus.Buildings = new Building[] { buildingModel_Temp.TogbXML(buildingModel_Temp.Name, buildingModel_Temp.Description, tolerance_Distance) };

            List<IPartition> partitions = buildingModel_Temp.GetPartitions();
            if (partitions != null)
            {
                List<Surface> surfaces = new List<Surface>();
                for (int i = 0; i < partitions.Count; i++)
                {
                    IPartition partition = partitions[i];
                    if (partition == null)
                        continue;

                    List<Space> spaces = buildingModel_Temp.GetSpaces(partition);
                    if (spaces != null && spaces.Count > 1)
                    {
                        //Spaces have to be in correct order!
                        //https://www.gbxml.org/schema_doc/6.01/GreenBuildingXML_Ver6.01.html#Link7

                        SortedDictionary<int, Space> sortedDictionary = new SortedDictionary<int, Space>();
                        spaces.ForEach(x => sortedDictionary[buildingModel.UniqueIndex(x)] = x);
                        spaces = sortedDictionary.Values.ToList();
                    }

                    Surface surface = buildingModel.TogbXML(partition, tolerance_Angle, tolerance_Distance);
                    if (surface != null)
                        surfaces.Add(surface);
                }
                campus.Surface = surfaces.ToArray();
            }

            return campus;
        }

    }
}
