using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.Spatial;
using System.Collections.Generic;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        public static Shell ToSAM(this ShellGeometry shellGeometry, double tolerance = Tolerance.Distance)
        {
            return ToSAM(shellGeometry?.ClosedShell, tolerance);
        }

        public static Shell ToSAM(this ClosedShell closedShell, double tolerance = Tolerance.Distance)
        {
            if (closedShell == null)
                return null;

            PolyLoop[] polyLoops = closedShell.PolyLoops;
            if (polyLoops == null)
                return null;

            List<Face3D> face3Ds = new List<Face3D>();
            foreach(PolyLoop polyLoop in polyLoops)
            {
                Polygon3D polygon3D = polyLoop.ToSAM(tolerance);
                if (polygon3D == null)
                    return null;

                face3Ds.Add(new Face3D(polygon3D));
            }

            return new Shell(face3Ds);
        }

    }
}
