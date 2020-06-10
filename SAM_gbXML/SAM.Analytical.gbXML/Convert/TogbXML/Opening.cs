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
        public static Opening TogbXML(this Aperture aperture, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (aperture == null)
                return null;

            PlanarBoundary3D planarBoundary3D = aperture.PlanarBoundary3D;
            if (planarBoundary3D == null)
                return null;

            ApertureConstruction apertureConstruction = aperture.ApertureConstruction;
            if (apertureConstruction == null)
                return null;
            
            Opening opening = new Opening();
            opening.constructionIdRef = aperture.ApertureConstruction.Guid.ToString();
            opening.Description = opening.Name;
            opening.id = aperture.Guid.ToString();
            opening.Name = aperture.Name;
            opening.openingType = Query.OpeningTypeEnum(aperture.ApertureConstruction.ApertureType);
            opening.pg = planarBoundary3D.TogbXML(tolerance);
            opening.rg = planarBoundary3D.TogbXML_RectangularGeometry(tolerance);

            return opening;
        }

    }
}
