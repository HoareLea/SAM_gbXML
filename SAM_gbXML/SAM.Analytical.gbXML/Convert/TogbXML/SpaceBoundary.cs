﻿using gbXMLSerializer;
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
        public static SpaceBoundary TogbXML_SpaceBoundary(this Panel panel, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (panel == null)
                return null;

            Geometry.Spatial.Face3D face3D = panel.PlanarBoundary3D.GetFace3D();
            if (face3D == null)
                return null;

            SpaceBoundary spaceBoundary = new SpaceBoundary();
            spaceBoundary.surfaceIdRef = panel.Guid.ToString();
            spaceBoundary.PlanarGeometry = face3D.TogbXML(tolerance);

            return spaceBoundary;
        }

    }
}
