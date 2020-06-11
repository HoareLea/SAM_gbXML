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
        public static Surface TogbXML(this Panel panel, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (panel == null)
                return null;

            PlanarBoundary3D planarBoundary3D = panel.PlanarBoundary3D;
            if (planarBoundary3D == null)
                return null;
            
            Surface surface = new Surface();
            surface.Name = panel.Name;
            surface.id = Core.gbXML.Query.Id(panel, typeof(Surface));
            //surface.constructionIdRef = Core.gbXML.Query.Id(panel.Construction, typeof(gbXMLSerializer.Construction));
            surface.CADObjectId = new CADObjectId() { id = panel.Guid.ToString() };
            surface.surfaceType = panel.PanelType.SurfaceTypeEnum();
            surface.RectangularGeometry = planarBoundary3D.TogbXML_RectangularGeometry(tolerance);
            surface.PlanarGeometry = planarBoundary3D.TogbXML(tolerance);
            surface.exposedToSunField = Query.ExposedToSun(panel.PanelType);

            List<Aperture> apertures = panel.Apertures;
            if(apertures != null)
                surface.Opening = apertures.ConvertAll(x => x.TogbXML(tolerance)).ToArray();

            return surface;
        }

    }
}
