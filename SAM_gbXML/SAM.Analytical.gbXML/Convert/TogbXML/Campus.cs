using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Campus TogbXML_Campus(this AnalyticalModel analyticalModel, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (analyticalModel == null)
                return null;

            Campus campus = new Campus();
            campus.id = Core.gbXML.Query.Id(analyticalModel, typeof(Campus));
            campus.Location = Core.gbXML.Convert.TogbXML(analyticalModel.Location, analyticalModel.Address, tolerance);

            AdjacencyCluster adjacencyCluster = analyticalModel.AdjacencyCluster;
            if(adjacencyCluster != null)
            {
                campus.Buildings = new Building[] { adjacencyCluster.TogbXML(analyticalModel.Name, analyticalModel.Description, tolerance) };
                List<Panel> panels = adjacencyCluster.GetPanels();
                if(panels != null)
                {
                    List<Surface> surfaces = new List<Surface>();
                    for(int i=0; i < panels.Count; i++)
                    {
                        Panel panel = panels[i];
                        if (panel == null)
                            continue;

                        List<Space> spaces = adjacencyCluster.GetRelatedObjects<Space>(panel);

                        Surface surface = panel.TogbXML(spaces, i + 1, tolerance);
                        if (surface != null)
                            surfaces.Add(surface);

                    }
                    campus.Surface = surfaces.ToArray();
                }
            }
 
            return campus;
        }

    }
}
