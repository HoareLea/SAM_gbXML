using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static surfaceTypeEnum? SurfaceTypeEnum(this ArchitecturalModel architecturalModel, IPartition partition, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.Distance)
        {
            PartitionAnalyticalType partitionAnalyticalType = architecturalModel.PartitionAnalyticalType(partition, tolerance_Angle, tolerance_Distance);
            
            if(partitionAnalyticalType == PartitionAnalyticalType.Undefined)
            {
                return null;
            }

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

            return surfaceTypeEnum.Air;
        }
    }
}