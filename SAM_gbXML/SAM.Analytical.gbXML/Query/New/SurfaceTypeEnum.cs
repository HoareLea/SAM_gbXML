using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Provides methods for querying the gbXML Building Model data.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Returns the surface type enum of the given partition in the building model.
        /// </summary>
        /// <param name="buildingModel">The gbXML building model.</param>
        /// <param name="partition">The partition to get the surface type of.</param>
        /// <param name="tolerance_Angle">Optional tolerance for angle comparison.</param>
        /// <param name="tolerance_Distance">Optional tolerance for distance comparison.</param>
        /// <returns>The surface type enum of the partition, or null if undefined.</returns>
        public static surfaceTypeEnum? SurfaceTypeEnum(this BuildingModel buildingModel, IPartition partition, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.Distance)
        {
            // Get the analytical type of the partition.
            PartitionAnalyticalType partitionAnalyticalType = buildingModel.PartitionAnalyticalType(partition, tolerance_Angle, tolerance_Distance);

            // If the analytical type is undefined, return null.
            if (partitionAnalyticalType == PartitionAnalyticalType.Undefined)
            {
                return null;
            }

            // Return the surface type enum based on the partition analytical type.
            switch (partitionAnalyticalType)
            {
                case PartitionAnalyticalType.UndergroundCeiling:
                    return surfaceTypeEnum.Ceiling;

                case PartitionAnalyticalType.CurtainWall:
                    return surfaceTypeEnum.ExteriorWall;

                case PartitionAnalyticalType.ExternalFloor:
                    return surfaceTypeEnum.ExposedFloor;

                case PartitionAnalyticalType.InternalFloor:
                    return surfaceTypeEnum.InteriorFloor;

                case PartitionAnalyticalType.Roof:
                    return surfaceTypeEnum.Roof;

                case PartitionAnalyticalType.Shade:
                    return surfaceTypeEnum.Shade;

                case PartitionAnalyticalType.OnGradeFloor:
                    return surfaceTypeEnum.SlabOnGrade;

                case PartitionAnalyticalType.UndergroundFloor:
                    return surfaceTypeEnum.UndergroundSlab;

                case PartitionAnalyticalType.UndergroundWall:
                    return surfaceTypeEnum.UndergroundWall;

                case PartitionAnalyticalType.ExternalWall:
                    return surfaceTypeEnum.ExteriorWall;

                case PartitionAnalyticalType.InternalWall:
                    return surfaceTypeEnum.InteriorWall;
            }

            // Return the default surface type enum.
            return surfaceTypeEnum.Air;
        }
    }
}
