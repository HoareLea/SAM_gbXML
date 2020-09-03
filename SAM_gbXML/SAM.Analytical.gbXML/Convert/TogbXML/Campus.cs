using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Campus TogbXML_Campus(this AnalyticalModel analyticalModel, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.Distance)
        {
            if (analyticalModel == null)
                return null;

            Campus campus = new Campus();
            campus.id = Core.gbXML.Query.Id(analyticalModel, typeof(Campus));
            campus.Location = Core.gbXML.Convert.TogbXML(analyticalModel.Location, analyticalModel.Address, tolerance);

            AdjacencyCluster adjacencyCluster = analyticalModel.AdjacencyCluster;
            if(adjacencyCluster != null)
            {
                adjacencyCluster = adjacencyCluster.SplitByInternalEdges(tolerance);
                adjacencyCluster = adjacencyCluster.UpdateNormals(false, silverSpacing, tolerance);
                
                campus.Buildings = new Building[] { adjacencyCluster.TogbXML(analyticalModel.Name, analyticalModel.Description, tolerance) };
                List<Panel> panels = adjacencyCluster.GetPanels();
                if(panels != null)
                {
                    int count_opening = 1;
                    List<Surface> surfaces = new List<Surface>();
                    for(int i=0; i < panels.Count; i++)
                    {
                        Panel panel = panels[i];
                        if (panel == null)
                            continue;

                        List<Space> spaces = adjacencyCluster.GetRelatedObjects<Space>(panel);

                        Surface surface = panel.TogbXML(spaces, i + 1, count_opening, tolerance);
                        if (surface != null)
                            surfaces.Add(surface);

                        if (surface.Opening != null)
                            count_opening += surface.Opening.Length;

                    }
                    campus.Surface = surfaces.ToArray();
                }
            }
 
            return campus;
        }

    }
}
