using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Opening TogbXML(this ArchitecturalModel architecturalModel, IOpening opening, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (architecturalModel == null || opening == null)
                return null;

            //ApertureConstruction apertureConstruction = aperture.ApertureConstruction;
            //if (apertureConstruction == null)
            //    return null;

            openingTypeEnum? openingTypeEnum = opening.OpeningTypeEnum();
            if (openingTypeEnum == null || !openingTypeEnum.HasValue)
            {
                return null;
            }

            string name = opening.Name;
            if (string.IsNullOrWhiteSpace(name))
                name = string.Empty;

            Opening result = new Opening();
            //opening.constructionIdRef = Core.gbXML.Query.Id(aperture.ApertureConstruction, typeof(gbXMLSerializer.Construction));
            result.Description = name;
            result.id = Core.gbXML.Query.Id(opening, typeof(Opening));
            result.Name = string.Format("{0} [{1}]", opening.Name == null ? string.Empty : opening.Name, opening.Guid).Trim();
            result.openingType = openingTypeEnum.Value;
            result.rg = Geometry.gbXML.Convert.TogbXML_RectangularGeometry(opening, tolerance);
            result.pg = Geometry.gbXML.Convert.TogbXML_PlanarGeometry(opening, tolerance);
            result.CADObjectId = architecturalModel.CADObjectId(opening);

            return result;
        }

    }
}
