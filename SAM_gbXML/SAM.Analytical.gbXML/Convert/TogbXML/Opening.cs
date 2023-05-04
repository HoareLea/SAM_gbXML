using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts an Aperture object to a gbXML Opening object.
        /// </summary>
        /// <param name="aperture">The Aperture object to convert.</param>
        /// <param name="cADObjectIdSufix">The suffix to append to the CAD object ID. Default value is -1.</param>
        /// <param name="tolerance">The tolerance for comparison. Default value is Core.Tolerance.MicroDistance.</param>
        /// <returns>A gbXML Opening object that corresponds to the input Aperture object.</returns>
        public static Opening TogbXML(this Aperture aperture, int cADObjectIdSufix = -1, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (aperture == null)
                return null;

            PlanarBoundary3D planarBoundary3D = aperture.PlanarBoundary3D;
            if (planarBoundary3D == null)
                return null;

            ApertureConstruction apertureConstruction = aperture.ApertureConstruction;
            if (apertureConstruction == null)
                return null;

            string name = aperture.Name;
            if (string.IsNullOrWhiteSpace(name))
                name = aperture.ApertureConstruction.Name;

            Opening opening = new Opening();
            opening.constructionIdRef = Core.gbXML.Query.Id(aperture.ApertureConstruction, typeof(gbXMLSerializer.WindowType));
            opening.Description = name;
            opening.id = Core.gbXML.Query.Id(aperture, typeof(Opening));

            // Generate a name for the Opening object
            if (cADObjectIdSufix == -1)
                opening.Name = string.Format("{0} [{1}]", name, aperture.Guid);
            else
                opening.Name = string.Format("{0} [{1}]", name, cADObjectIdSufix);

            // Set the opening type based on the ApertureConstruction object
            opening.openingType = Query.OpeningTypeEnum(aperture.ApertureConstruction.ApertureType);

            // Convert the PlanarBoundary3D object to a gbXML PlanarGeometry object
            opening.pg = planarBoundary3D.TogbXML(tolerance);

            // Convert the PlanarBoundary3D object to a gbXML RectangularGeometry object
            opening.rg = planarBoundary3D.TogbXML_RectangularGeometry(tolerance);

            // Generate the CAD object ID for the Opening object
            opening.CADObjectId = Query.CADObjectId(aperture, cADObjectIdSufix);

            return opening;
        }
    }
}
