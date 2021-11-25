using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static CADObjectId CADObjectId(this BuildingModel buildingModel, IPartition partition, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.Distance)
        {
            if (buildingModel == null)
            {
                return null;
            }

            int index = buildingModel.UniqueIndex(partition);
            if (index == -1)
            {
                return null;
            }

            PartitionAnalyticalType partitionAnalyticalType = buildingModel.PartitionAnalyticalType(partition, tolerance_Angle, tolerance_Distance);
            if (partitionAnalyticalType == PartitionAnalyticalType.Undefined)
            {
                return null;
            }

            if(partitionAnalyticalType == PartitionAnalyticalType.Shade)
            {
                if(partition is Wall)
                {
                    partitionAnalyticalType = PartitionAnalyticalType.ExternalWall;
                }
                else if(partition is Roof)
                {
                    partitionAnalyticalType = PartitionAnalyticalType.Roof;
                }
                else if (partition is Floor)
                {
                    partitionAnalyticalType = PartitionAnalyticalType.ExternalFloor;
                }
            }

            string prefix = null;
            switch (partitionAnalyticalType)
            {
                case PartitionAnalyticalType.Air:
                    prefix = "Air";
                    break;

                case PartitionAnalyticalType.CurtainWall:
                    prefix = "Curtain Wall";
                    break;

                case PartitionAnalyticalType.ExternalFloor:
                case PartitionAnalyticalType.InternalFloor:
                case PartitionAnalyticalType.OnGradeFloor:
                case PartitionAnalyticalType.UndergroundFloor:
                    prefix = "Floor";
                    break;

                case PartitionAnalyticalType.Roof:
                    prefix = "Basic Roof";
                    break;

                case PartitionAnalyticalType.ExternalWall:
                case PartitionAnalyticalType.InternalWall:
                case PartitionAnalyticalType.UndergroundWall:
                    prefix = "Basic Wall";
                    break;
            }

            if (string.IsNullOrWhiteSpace(prefix))
            {
                return null;
            }

            string name = partition.Name;
            if(name == null)
            {
                name = string.Empty;
            }
            
            
            if(!name.StartsWith(prefix))
            {
                name = string.Format("{0}: {1}", prefix, name);
            }

            name = name.Trim();

            name = string.Format("{0} [{1}]", name, index.ToString());

            CADObjectId result = new CADObjectId()
            {
                id = name
            };

            return result;
        }

        public static CADObjectId CADObjectId(this BuildingModel buildingModel, IOpening opening)
        {
            if (buildingModel == null)
            {
                return null;
            }

            int index = buildingModel.UniqueIndex(opening);
            if (index == -1)
            {
                return null;
            }

            OpeningAnalyticalType openingAnalyticalType = opening.OpeningAnalyticalType();
            if(openingAnalyticalType == OpeningAnalyticalType.Undefined)
            {
                return null;
            }

            string prefix = null;
            switch (openingAnalyticalType)
            {
                case OpeningAnalyticalType.Window:
                    prefix = "Windows";
                    break;

                case OpeningAnalyticalType.Door:
                    prefix = "Doors";
                    break;
            }

            if (string.IsNullOrWhiteSpace(prefix))
            {
                return null;
            }

            string name = opening.Name;
            if (name == null)
            {
                name = string.Empty;
            }


            if (!name.StartsWith(prefix))
            {
                name = string.Format("{0}: {1}", prefix, name);
            }

            name = name.Trim();

            name = string.Format("{0} [{1}]", name, index.ToString());

            CADObjectId result = new CADObjectId()
            {
                id = name
            };

            return result;

        }
    }
}