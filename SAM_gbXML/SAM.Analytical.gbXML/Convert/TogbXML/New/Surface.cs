using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A static class containing extension methods to convert BuildingModel objects to gbXML format.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts a BuildingModel object to a Surface object in gbXML format.
        /// </summary>
        /// <param name="buildingModel">The BuildingModel object to convert.</param>
        /// <param name="partition">The IPartition object to convert.</param>
        /// <param name="tolerance_Angle">The angle tolerance to use for the conversion.</param>
        /// <param name="tolerance_Distance">The distance tolerance to use for the conversion.</param>
        /// <returns>A Surface object in gbXML format.</returns>
        public static Surface TogbXML(this BuildingModel buildingModel, IPartition partition, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.MicroDistance)
        {
            // Check if the partition and buildingModel objects are not null.
            if (partition == null || buildingModel == null)
            {
                return null;
            }

            // Get the surface type enum from the buildingModel object.
            surfaceTypeEnum? surfaceTypeEnum = buildingModel.SurfaceTypeEnum(partition, tolerance_Angle, tolerance_Distance);
            if (surfaceTypeEnum == null || !surfaceTypeEnum.HasValue)
            {
                return null;
            }

            // Create a new Surface object.
            Surface surface = new Surface();

            // Set the name of the Surface object.
            surface.Name = string.Format("{0} [{1}]", partition.Name == null ? string.Empty : partition.Name, partition.Guid).Trim();

            // Set the ID of the Surface object.
            surface.id = Core.gbXML.Query.Id(partition, typeof(Surface));

            // Set the CAD object ID of the Surface object.
            surface.CADObjectId = Query.CADObjectId(buildingModel, partition, tolerance_Angle, tolerance_Distance);

            // Set the surface type of the Surface object.
            surface.surfaceType = surfaceTypeEnum.Value;

            // Set the rectangular geometry of the Surface object.
            surface.RectangularGeometry = Geometry.gbXML.Convert.TogbXML_RectangularGeometry(partition, tolerance_Distance);

            // Set the planar geometry of the Surface object.
            surface.PlanarGeometry = Geometry.gbXML.Convert.TogbXML_PlanarGeometry(partition, tolerance_Distance);

            // Set the exposed to sun field of the Surface object.
            surface.exposedToSunField = buildingModel.ExposedToSun(partition);

            // Get the spaces associated with the partition.
            List<Space> spaces = buildingModel.GetSpaces(partition);

            // If there are spaces associated with the partition, set the adjacent space IDs of the Surface object.
            if (spaces != null && spaces.Count > 0)
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

            // If the partition is a host partition, set the openings of the Surface object.
            if (partition is IHostPartition)
            {
                IHostPartition hostPartition = (IHostPartition)partition;
                List<IOpening> openings = hostPartition.GetOpenings();
                if (openings != null)
                {
                    List<Opening> openings_gbXML = new List<Opening>();
                    foreach (IOpening opening in openings)
                    {
                        Opening opening_gbXML = buildingModel.TogbXML(opening, tolerance_Distance);
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
