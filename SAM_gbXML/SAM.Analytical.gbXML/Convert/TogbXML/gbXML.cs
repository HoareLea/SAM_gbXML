using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static gbXMLSerializer.gbXML TogbXML(this AnalyticalModel analyticalModel, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (analyticalModel == null)
                return null;

            List<gbXMLSerializer.Construction> constructions_gbXML = new List<gbXMLSerializer.Construction>();

            List<Panel> panels = analyticalModel.AdjacencyCluster?.GetPanels();
            if(panels != null && panels.Count > 0)
            {
                HashSet<System.Guid> guids = new HashSet<System.Guid>();
                foreach(Panel panel in panels)
                {
                    Construction construction = panel?.Construction;
                    if (construction != null && !guids.Contains(construction.Guid))
                    {
                        gbXMLSerializer.Construction construction_gbXML = construction.TogbXML(tolerance);
                        if (construction_gbXML != null)
                        {
                            //constructions_gbXML.Add(construction_gbXML);
                            guids.Add(construction.Guid);
                        }

                    }

                    List<Aperture> apertures = panel?.Apertures;
                    if(apertures != null && apertures.Count > 0)
                    {
                        foreach(Aperture aperture in apertures)
                        {
                            ApertureConstruction apertureConstruction = aperture?.ApertureConstruction;
                            if (apertureConstruction == null)
                                continue;

                            gbXMLSerializer.Construction construction_gbXML = apertureConstruction.TogbXML(tolerance);
                            if (construction_gbXML != null)
                            {
                                //constructions_gbXML.Add(construction_gbXML);
                                guids.Add(apertureConstruction.Guid);
                            }
                        }
                    }
                }
            }

            gbXMLSerializer.gbXML gbXML = new gbXMLSerializer.gbXML();
            gbXML.useSIUnitsForResults = "true";
            gbXML.temperatureUnit = temperatureUnitEnum.C;
            gbXML.lengthUnit = lengthUnitEnum.Meters;
            gbXML.areaUnit = areaUnitEnum.SquareMeters;
            gbXML.volumeUnit = volumeUnitEnum.CubicMeters;
            gbXML.version = versionEnum.FiveOneOne;
            gbXML.Campus = analyticalModel.TogbXML_Campus(silverSpacing, tolerance);
            gbXML.Constructions = constructions_gbXML.ToArray();
            gbXML.DocumentHistory = Core.gbXML.Query.DocumentHistory(analyticalModel.Guid);

            return gbXML;
        }

    }
}
