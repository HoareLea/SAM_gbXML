//Import the gbXMLSerializer namespace
using gbXMLSerializer;

//Create a namespace for the class
namespace SAM.Analytical.gbXML
{
    //Create a static partial class named Convert
    public static partial class Convert
    {
        /// <summary>
        /// Converts an IOpening object to an Opening object for gbXML serialization
        /// </summary>
        /// <param name="buildingModel">The BuildingModel object containing the IOpening</param>
        /// <param name="opening">The IOpening object to convert</param>
        /// <param name="tolerance">The tolerance for converting geometric objects (default is Core.Tolerance.MicroDistance)</param>
        /// <returns>An Opening object ready for gbXML serialization</returns>
        public static Opening TogbXML(this BuildingModel buildingModel, IOpening opening, double tolerance = Core.Tolerance.MicroDistance)
        {
            //If either parameter is null, return null
            if (buildingModel == null || opening == null)
                return null;

            //Get the opening type
            openingTypeEnum? openingTypeEnum = opening.OpeningTypeEnum();
            if (openingTypeEnum == null || !openingTypeEnum.HasValue)
            {
                return null;
            }

            //Get the opening name or set it to an empty string if it's null or whitespace
            string name = opening.Name;
            if (string.IsNullOrWhiteSpace(name))
                name = string.Empty;

            //Create a new Opening object and set its properties
            Opening result = new Opening();
            result.Description = name;
            result.id = Core.gbXML.Query.Id(opening, typeof(Opening));
            result.Name = string.Format("{0} [{1}]", opening.Name == null ? string.Empty : opening.Name, opening.Guid).Trim();
            result.openingType = openingTypeEnum.Value;
            result.rg = Geometry.gbXML.Convert.TogbXML_RectangularGeometry(opening, tolerance);
            result.pg = Geometry.gbXML.Convert.TogbXML_PlanarGeometry(opening, tolerance);
            result.CADObjectId = buildingModel.CADObjectId(opening);

            return result;
        }
    }
}
