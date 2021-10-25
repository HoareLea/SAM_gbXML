using gbXMLSerializer;
using System.Collections.Generic;
using System.Linq;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Campus TogbXML_Campus(this ArchitecturalModel architecturalModel, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.Distance)
        {
            if (architecturalModel == null)
                return null;

            ArchitecturalModel architecturalModel_Temp = new ArchitecturalModel(architecturalModel);

            Campus campus = new Campus();
            campus.id = Core.gbXML.Query.Id(architecturalModel_Temp, typeof(Campus));
            campus.Location = Core.gbXML.Convert.TogbXML(architecturalModel_Temp.Location, architecturalModel_Temp.Address, tolerance_Distance);

            architecturalModel_Temp.SplitByInternalEdges(tolerance_Distance);
            architecturalModel_Temp.OrientPartitions(false, silverSpacing, tolerance_Distance);
            architecturalModel_Temp.FixEdges(tolerance_Distance);

            campus.Buildings = new Building[] { architecturalModel_Temp.TogbXML(architecturalModel_Temp.Name, architecturalModel_Temp.Description, tolerance_Distance) };

            List<IPartition> partitions = architecturalModel_Temp.GetPartitions();
            if (partitions != null)
            {
                List<Surface> surfaces = new List<Surface>();
                for (int i = 0; i < partitions.Count; i++)
                {
                    IPartition partition = partitions[i];
                    if (partition == null)
                        continue;

                    List<Space> spaces = architecturalModel_Temp.GetSpaces(partition);
                    if (spaces != null && spaces.Count > 1)
                    {
                        //Spaces have to be in correct order!
                        //https://www.gbxml.org/schema_doc/6.01/GreenBuildingXML_Ver6.01.html#Link7

                        SortedDictionary<int, Space> sortedDictionary = new SortedDictionary<int, Space>();
                        spaces.ForEach(x => sortedDictionary[architecturalModel.UniqueIndex(x)] = x);
                        spaces = sortedDictionary.Values.ToList();
                    }

                    Surface surface = architecturalModel.TogbXML(partition, tolerance_Angle, tolerance_Distance);
                    if (surface != null)
                        surfaces.Add(surface);
                }
                campus.Surface = surfaces.ToArray();
            }

            return campus;
        }

    }
}
