// This file contains a static class that provides conversion methods for gbXMLSerializer.SpaceBoundary and gbXMLSerializer.Surface objects to SAM.Analytical.Panel objects
// The class name is Convert and is defined within the SAM.Analytical.gbXML namespace
// The file imports the gbXMLSerializer, SAM.Core, SAM.Geometry.gbXML, SAM.Geometry.Spatial, and System.Collections.Generic namespaces
using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
 
    public static partial class Convert
    {
        /// <summary>
        /// Converts a gbXMLSerializer.SpaceBoundary object to a SAM.Analytical.Panel object.
        /// </summary>
        /// <param name="spaceBoundary">The gbXML space boundary object to convert.</param>
        /// <param name="tolerance">The tolerance value to use for the conversion.</param>
        /// <returns>A SAM.Analytical.Panel object containing the converted gbXML space </returns>
        public static Panel ToSAM(this SpaceBoundary spaceBoundary, double tolerance = Tolerance.Distance)
        {
            if (spaceBoundary == null)
                return null;

            Polygon3D polygon3D = spaceBoundary.PlanarGeometry.ToSAM(tolerance);
            if (polygon3D == null)
                return null;

            return Analytical.Create.Panel(null, PanelType.Undefined, new Face3D(polygon3D));
        }

        /// <summary>
        /// Converts a gbXMLSerializer.Surface object to a SAM.Analytical.Panel object.
        /// </summary>
        /// <param name="surface">The gbXML surface object to convert.</param>
        /// <param name="constructions">A collection of Construction objects to use for setting the panel construction. If null, the construction will be set based on the surface's constructionIdRef property.</param>
        /// <param name="tolerance">The tolerance value to use for the conversion.</param>
        /// <returns>A SAM.Analytical.Panel object containing the converted gbXML surface data.</returns>
        public static Panel ToSAM(this gbXMLSerializer.Surface surface, IEnumerable<Construction> constructions = null,  double tolerance = Tolerance.Distance)
        {
            if (surface == null)
                return null;

            Polygon3D polygon3D = surface.PlanarGeometry.ToSAM(tolerance);
            if (polygon3D == null)
                return null;

            PanelType panelType = Query.PanelType(surface.surfaceType);

            Construction construction = constructions?.Construction(surface.constructionIdRef);
            if(construction == null)
            {
                construction = string.IsNullOrWhiteSpace(surface.constructionIdRef) ? new Construction(surface.Name) : new Construction(surface.constructionIdRef);
            }

            Panel result = Analytical.Create.Panel(construction, panelType, new Face3D(polygon3D));
            result.SetValue(PanelParameter.Id, surface.id);

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
