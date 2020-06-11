using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Campus TogbXML_Campus(this AnalyticalModel analyticalModel, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (analyticalModel == null)
                return null;

            Campus campus = new Campus();
            campus.id = analyticalModel.Guid.ToString();
            campus.Location = analyticalModel?.Location?.TogbXML(analyticalModel.Address, tolerance);

            AdjacencyCluster adjacencyCluster = analyticalModel.AdjacencyCluster;
            if(adjacencyCluster != null)
            {
                campus.Buildings = new Building[] { adjacencyCluster.TogbXML(tolerance) };
                campus.Surface = adjacencyCluster.GetPanels()?.ConvertAll(x => x.TogbXML(tolerance)).ToArray();
            }
 
            return campus;
        }

    }
}
