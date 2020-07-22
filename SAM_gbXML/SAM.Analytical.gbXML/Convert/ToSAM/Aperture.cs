using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Aperture ToSAM(this gbXMLSerializer.Opening opening, double tolerance = Tolerance.MicroDistance)
        {
            if (opening == null)
                return null;

            Polygon3D polygon3D = opening.pg.ToSAM(tolerance);
            if (polygon3D == null)
                return null;

            ApertureType apertureType = Query.ApertureType(opening.openingType);

            ApertureConstruction apertureConstruction = new ApertureConstruction(opening.Name, apertureType);

            Aperture result = new Aperture(apertureConstruction, polygon3D);

            return result;
        }

    }
}
