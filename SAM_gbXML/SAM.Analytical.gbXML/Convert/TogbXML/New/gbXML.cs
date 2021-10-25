using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static gbXMLSerializer.gbXML TogbXML(this ArchitecturalModel architecturalModel, double silverSpacing = Core.Tolerance.MacroDistance, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.MicroDistance)
        {
            if (architecturalModel == null)
                return null;

            List<gbXMLSerializer.Construction> constructions = new List<gbXMLSerializer.Construction>();

            List<HostPartitionType> hostPartitionTypes = architecturalModel.GetHostPartitionTypes();
            if(hostPartitionTypes != null && hostPartitionTypes.Count != 0)
            {
                foreach (HostPartitionType hostPartitionType in hostPartitionTypes)
                {
                    gbXMLSerializer.Construction construction = hostPartitionType.TogbXML();
                    if(construction != null)
                    {
                        //constructions.Add(construction);
                    }
                }
            }

            List<OpeningType> openiengTypes = architecturalModel.GetOpeningTypes();
            if(openiengTypes!= null && openiengTypes.Count != 0)
            {
                foreach(OpeningType openingType in openiengTypes)
                {
                    gbXMLSerializer.Construction construction = openingType.TogbXML();
                    if (construction != null)
                    {
                        //constructions.Add(construction);
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
            gbXML.Campus = architecturalModel.TogbXML_Campus(silverSpacing, tolerance_Angle, tolerance_Distance);
            gbXML.Constructions = constructions.ToArray();
            gbXML.DocumentHistory = Core.gbXML.Query.DocumentHistory(architecturalModel.Guid);

            return gbXML;
        }

    }
}
