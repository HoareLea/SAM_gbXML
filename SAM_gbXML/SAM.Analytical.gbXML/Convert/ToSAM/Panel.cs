using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Panel ToSAM(this SpaceBoundary spaceBoundary, double tolerance = Tolerance.MicroDistance)
        {
            if (spaceBoundary == null)
                return null;

            Polygon3D polygon3D = spaceBoundary.PlanarGeometry.ToSAM();
            if (polygon3D == null)
                return null;

            return Analytical.Create.Panel(null, PanelType.Undefined, new Face3D(polygon3D));
        }

        public static Panel ToSAM(this gbXMLSerializer.Surface surface, double tolerance = Tolerance.MicroDistance)
        {
            if (surface == null)
                return null;

            Polygon3D polygon3D = surface.PlanarGeometry.ToSAM(tolerance);
            if (polygon3D == null)
                return null;

            PanelType panelType = Query.PanelType(surface.surfaceType);

            Construction construction = new Construction(surface.Name);

            Panel result = Analytical.Create.Panel(construction, panelType, new Face3D(polygon3D));

            Opening[] openings = surface.Opening;
            if(openings != null)
            {
                foreach(Opening opening in openings)
                {
                    Aperture aperture = opening.ToSAM(tolerance);
                    if (aperture != null)
                        result.AddAperture(aperture);
                }
            }

            return result;
        }

    }
}
