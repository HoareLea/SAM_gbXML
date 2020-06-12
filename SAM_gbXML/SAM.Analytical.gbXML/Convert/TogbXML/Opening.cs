using gbXMLSerializer;
using SAM.Geometry.gbXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
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
            //opening.constructionIdRef = Core.gbXML.Query.Id(aperture.ApertureConstruction, typeof(gbXMLSerializer.Construction));
            opening.Description = name;
            opening.id = Core.gbXML.Query.Id(aperture, typeof(Opening));
            opening.Name = string.Format("{0} [{1}]", name, aperture.Guid); ;
            opening.openingType = Query.OpeningTypeEnum(aperture.ApertureConstruction.ApertureType);
            opening.pg = planarBoundary3D.TogbXML(tolerance);
            opening.rg = planarBoundary3D.TogbXML_RectangularGeometry(tolerance);
            opening.CADObjectId = Query.CADObjectId(aperture, cADObjectIdSufix);

            return opening;
        }

    }
}
