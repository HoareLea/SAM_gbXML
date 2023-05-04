using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.Spatial;
using System.Collections.Generic;

namespace SAM.Geometry.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts ShellGeometry to SAM Geometry Spatial Shell.
        /// </summary>
        /// <param name="shellGeometry">Input ShellGeometry.</param>
        /// <param name="tolerance">Tolerance.</param>
        /// <returns>Returns converted SAM Geometry Spatial Shell.</returns>
        public static Shell ToSAM(this ShellGeometry shellGeometry, double tolerance = Tolerance.Distance)
        {
            return ToSAM(shellGeometry?.ClosedShell, tolerance);
        }

        /// <summary>
        /// Converts ClosedShell to SAM Geometry Spatial Shell.
        /// </summary>
        /// <param name="closedShell">Input ClosedShell.</param>
        /// <param name="tolerance">Tolerance.</param>
        /// <returns>Returns converted SAM Geometry Spatial Shell.</returns>
        public static Shell ToSAM(this ClosedShell closedShell, double tolerance = Tolerance.Distance)
        {
            if (closedShell == null)
                return null;

            PolyLoop[] polyLoops = closedShell.PolyLoops;
            if (polyLoops == null)
                return null;

            List<Face3D> face3Ds = new List<Face3D>();
            foreach (PolyLoop polyLoop in polyLoops)
            {
                // Convert each PolyLoop to a SAM Polygon3D
                Polygon3D polygon3D = polyLoop.ToSAM(tolerance);
                if (polygon3D == null)
                    return null;

                // Create a Face3D from the Polygon3D and add it to the list of faces
                face3Ds.Add(new Face3D(polygon3D));
            }

            // Create a Shell from the list of faces
            return new Shell(face3Ds);
        }

    }
}
