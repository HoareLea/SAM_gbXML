// Import required libraries
using gbXMLSerializer;
using System.Collections.Generic;

// Create a static class called Query within the SAM.Analytical.gbXML namespace
namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// A static partial class that contains extension methods for querying and manipulating BuildingModel data.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Gets a unique CADObjectId for a given partition within a building model.
        /// </summary>
        /// <param name="buildingModel">The BuildingModel that contains the partition</param>
        /// <param name="partition">The partition for which to generate a CADObjectId</param>
        /// <param name="tolerance_Angle">The angular tolerance used to determine partition type</param>
        /// <param name="tolerance_Distance">The distance tolerance used to determine partition type</param>
        /// <returns>A unique CADObjectId for the partition, or null if the partition cannot be uniquely identified</returns>
        public static CADObjectId CADObjectId(this BuildingModel buildingModel, IPartition partition, double tolerance_Angle = Core.Tolerance.Angle, double tolerance_Distance = Core.Tolerance.Distance)
        {
            // Return null if the building model is null
            if (buildingModel == null)
            {
                return null;
            }

            // Get the unique index of the partition within the building model
            int index = buildingModel.UniqueIndex(partition);

            // Return null if the partition cannot be uniquely identified
            if (index == -1)
            {
                return null;
            }

            // Determine the partition's analytical type based on its shape and orientation
            PartitionAnalyticalType partitionAnalyticalType = buildingModel.PartitionAnalyticalType(partition, tolerance_Angle, tolerance_Distance);

            // Return null if the partition's analytical type is undefined
            if (partitionAnalyticalType == PartitionAnalyticalType.Undefined)
            {
                return null;
            }

            // If the partition's analytical type is Shade, determine its actual type based on its IPartition subtype
            if (partitionAnalyticalType == PartitionAnalyticalType.Shade)
            {
                if (partition is Wall)
                {
                    partitionAnalyticalType = PartitionAnalyticalType.ExternalWall;
                }
                else if (partition is Roof)
                {
                    partitionAnalyticalType = PartitionAnalyticalType.Roof;
                }
                else if (partition is Floor)
                {
                    partitionAnalyticalType = PartitionAnalyticalType.ExternalFloor;
                }
            }

            // Determine the prefix string for the partition's analytical type
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

            // Return null if the prefix string is null or empty
            if (string.IsNullOrWhiteSpace(prefix))
            {
                return null;
            }

            // Get the name of the partition, or set it to an empty string if it is null
            string name = partition.Name;
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

        /// <summary>
        /// Extension method to generate a CADObjectId for a given BuildingModel and IOpening.
        /// </summary>
        /// <param name="buildingModel">The BuildingModel instance.</param>
        /// <param name="opening">The IOpening instance.</param>
        /// <returns>The generated CADObjectId.</returns>
        public static CADObjectId CADObjectId(this BuildingModel buildingModel, IOpening opening)
        {
            // Check if BuildingModel is null, return null if so
            if (buildingModel == null)
            {
                return null;
            }

            // Get the index of the opening within the BuildingModel
            int index = buildingModel.UniqueIndex(opening);

            // If the opening is not found in the BuildingModel, return null
            if (index == -1)
            {
                return null;
            }

            // Get the opening analytical type
            OpeningAnalyticalType openingAnalyticalType = opening.OpeningAnalyticalType();

            // If the opening analytical type is undefined, return null
            if (openingAnalyticalType == OpeningAnalyticalType.Undefined)
            {
                return null;
            }

            // Determine the prefix based on the opening analytical type
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

            // If the prefix is null or whitespace, return null
            if (string.IsNullOrWhiteSpace(prefix))
            {
                return null;
            }

            // Get the name of the opening
            string name = opening.Name;

            // If the name is null, set it to an empty string
            if (name == null)
            {
                name = string.Empty;
            }

            // If the name doesn't start with the prefix, prepend the prefix to the name
            if (!name.StartsWith(prefix))
            {
                name = string.Format("{0}: {1}", prefix, name);
            }

            // Trim any leading or trailing whitespace from the name
            name = name.Trim();

            // Append the index to the name
            name = string.Format("{0} [{1}]", name, index.ToString());

            // Create a new CADObjectId with the name as its id
            CADObjectId result = new CADObjectId()
            {
                id = name
            };

            // Return the result
            return result;
        }
    }
}