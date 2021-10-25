using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Surface TogbXML(this ArchitecturalModel architecturalModel, IPartition partition, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.MicroDistance)
        {
            if (partition == null || architecturalModel == null)
            {
                return null;
            }

            surfaceTypeEnum? surfaceTypeEnum = architecturalModel.SurfaceTypeEnum(partition, tolerance_Angle, tolerance_Distance);
            if(surfaceTypeEnum == null || !surfaceTypeEnum.HasValue)
            {
                return null;
            }

            Surface surface = new Surface();
            surface.Name = string.Format("{0} [{1}]", partition.Name == null ? string.Empty : partition.Name, partition.Guid).Trim();
            surface.id = Core.gbXML.Query.Id(partition, typeof(Surface));
            //surface.constructionIdRef = Core.gbXML.Query.Id(panel.Construction, typeof(gbXMLSerializer.Construction));
            surface.CADObjectId = Query.CADObjectId(architecturalModel, partition, tolerance_Angle, tolerance_Distance);
            surface.surfaceType = surfaceTypeEnum.Value;
            surface.RectangularGeometry = Geometry.gbXML.Convert.TogbXML_RectangularGeometry(partition, tolerance_Distance);
            surface.PlanarGeometry = Geometry.gbXML.Convert.TogbXML_PlanarGeometry(partition, tolerance_Distance);
            surface.exposedToSunField = architecturalModel.ExposedToSun(partition);

            List<Space> spaces = architecturalModel.GetSpaces(partition);
            if(spaces != null && spaces.Count > 0)
            {
                List<AdjacentSpaceId> adjacentSpaceIds = new List<AdjacentSpaceId>();
                foreach (Space space in spaces)
                {
                    AdjacentSpaceId adjacentSpaceId = Query.AdjacentSpaceId(space);
                    if (adjacentSpaceId == null)
                        continue;
                    adjacentSpaceIds.Add(adjacentSpaceId);
                }
                surface.AdjacentSpaceId = adjacentSpaceIds.ToArray();
            }


            if(partition is IHostPartition)
            {
                IHostPartition hostPartition = (IHostPartition)partition;
                List<IOpening> openings = hostPartition.GetOpenings();
                if (openings != null)
                {
                    List<Opening> openings_gbXML = new List<Opening>();
                    foreach (IOpening opening in openings)
                    {
                        Opening opening_gbXML = architecturalModel.TogbXML(opening, tolerance_Distance);
                        if (opening_gbXML == null)
                        {
                            continue;
                        }

                        openings.Add(opening);
                    }
                    surface.Opening = openings_gbXML.ToArray();
                }
            }

            return surface;
        }

    }
}
