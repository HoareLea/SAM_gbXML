using gbXMLSerializer;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Surface TogbXML(this Panel panel, List<Space> adjacentSpaces = null, int cADObjectIdSufix = -1, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (panel == null)
                return null;

            PlanarBoundary3D planarBoundary3D = panel.PlanarBoundary3D;
            if (planarBoundary3D == null)
                return null;
            
            Surface surface = new Surface();
            surface.Name = string.Format("{0} [{1}]", panel.Name, panel.Guid);
            surface.id = Core.gbXML.Query.Id(panel, typeof(Surface));
            surface.constructionIdRef = Core.gbXML.Query.Id(panel.Construction, typeof(gbXMLSerializer.Construction));
            surface.CADObjectId = Query.CADObjectId(panel, cADObjectIdSufix);
            surface.surfaceType = panel.PanelType.SurfaceTypeEnum();
            surface.RectangularGeometry = planarBoundary3D.TogbXML_RectangularGeometry(tolerance);
            surface.PlanarGeometry = planarBoundary3D.TogbXML(tolerance);
            surface.exposedToSunField = Query.ExposedToSun(panel.PanelType);

            if(adjacentSpaces != null && adjacentSpaces.Count > 0)
            {
                List<AdjacentSpaceId> adjacentSpaceIds = new List<AdjacentSpaceId>();
                foreach (Space space in adjacentSpaces)
                {
                    AdjacentSpaceId adjacentSpaceId = Query.AdjacentSpaceId(space);
                    if (adjacentSpaceId == null)
                        continue;
                    adjacentSpaceIds.Add(adjacentSpaceId);
                }
                surface.AdjacentSpaceId = adjacentSpaceIds.ToArray();
            }

            List<Aperture> apertures = panel.Apertures;
            if(apertures != null)
                surface.Opening = apertures.ConvertAll(x => x.TogbXML(tolerance)).ToArray();

            return surface;
        }

    }
}
