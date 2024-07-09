using gbXMLSerializer;
using SAM.Geometry.gbXML;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Convert a Panel to a Surface for gbXML serialization
        /// </summary>
        /// <param name="panel">The Panel to convert</param>
        /// <param name="adjacentSpaces">List of adjacent spaces, if any</param>
        /// <param name="cADObjectIdSufix_Surface">Suffix to add to CAD Object ID of the surface</param>
        /// <param name="cADObjectIdSufix_Opening">Suffix to add to CAD Object ID of openings</param>
        /// <param name="tolerance">Tolerance value for geometric calculations</param>
        /// <returns>A Surface object for gbXML serialization</returns>
        public static Surface TogbXML(this IPanel panel, List<ISpace> adjacentSpaces = null, int cADObjectIdSufix_Surface = -1, int cADObjectIdSufix_Opening = -1, bool flipSurface = false, double tolerance = Core.Tolerance.MicroDistance)
        {
            // Return null if the panel is null
            if (panel == null)
                return null;

            // Get the planar boundary of the panel
            SAM.Geometry.Spatial.Face3D face3D = panel.Face3D;
            if (face3D == null)
                return null;

            if(flipSurface)
            {
                face3D.FlipNormal();
            }

            // Create a new Surface object
            Surface surface = new Surface();

            string name = panel is Panel ? ((Panel)panel).Name : panel is ExternalPanel ? ((ExternalPanel)panel).Name : null;

            // Set the name of the surface based on the Panel's name and ID
            if (cADObjectIdSufix_Surface == -1)
                surface.Name = string.Format("{0} [{1}]", name, panel.Guid);
            else
                surface.Name = string.Format("{0} [{1}]", name, cADObjectIdSufix_Surface);

            // Set the ID, construction ID, CAD object ID, and surface type of the surface
            surface.id = Core.gbXML.Query.Id(panel, typeof(Surface));
            surface.constructionIdRef = panel is Panel ? Core.gbXML.Query.Id(((Panel)panel).Construction, typeof(gbXMLSerializer.Construction)) : panel is ExternalPanel ? Core.gbXML.Query.Id(((ExternalPanel)panel).Construction, typeof(gbXMLSerializer.Construction)) : null;
            surface.CADObjectId = Query.CADObjectId(panel, cADObjectIdSufix_Surface);
            surface.surfaceType = panel is Panel ? ((Panel)panel).PanelType.SurfaceTypeEnum() : surfaceTypeEnum.Air;

            // Add the rectangular and planar geometry to the surface
            surface.RectangularGeometry = face3D.TogbXML_RectangularGeometry(tolerance);
            surface.PlanarGeometry = face3D.TogbXML(tolerance);

            // Check if the panel is exposed to the sun and set the value accordingly
            surface.exposedToSunField = panel is Panel ? Analytical.Query.ExposedToSun(((Panel)panel).PanelType) : true;

            // Add adjacent space IDs to the surface, if provided
            if (adjacentSpaces != null && adjacentSpaces.Count > 0)
            {
                List<AdjacentSpaceId> adjacentSpaceIds = new List<AdjacentSpaceId>();
                foreach (ISpace space in adjacentSpaces)
                {
                    // Get the AdjacentSpaceId object for the space and add it to the list
                    AdjacentSpaceId adjacentSpaceId = Query.AdjacentSpaceId(space);
                    if (adjacentSpaceId == null)
                        continue;
                    adjacentSpaceIds.Add(adjacentSpaceId);
                }
                surface.AdjacentSpaceId = adjacentSpaceIds.ToArray();
            }

            // Add openings to the surface, if any exist
            if(panel is Panel)
            {
                List<Aperture> apertures = ((Panel)panel).Apertures;
                if (apertures != null)
                {
                    List<Opening> openings = new List<Opening>();

                    int cADObjectIdSufix_Opening_Temp = cADObjectIdSufix_Opening;
                    foreach (Aperture aperture in apertures)
                    {
                        // Convert each aperture to an Opening and add it to the list
                        Opening opening = aperture.TogbXML(cADObjectIdSufix_Opening_Temp, tolerance);
                        if (opening == null)
                            continue;

                        if (cADObjectIdSufix_Opening_Temp != -1)
                            cADObjectIdSufix_Opening_Temp++;

                        openings.Add(opening);
                    }
                    surface.Opening = openings.ToArray();
                }
            }


            return surface;
        }

    }
}
