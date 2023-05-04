using gbXMLSerializer;
using SAM.Geometry.Spatial;
using System.Collections.Generic;

namespace SAM.Geometry.gbXML
{
    /// <summary>
    /// Contains methods to convert geometry objects to gbXML format.
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Converts an IClosedPlanar3D object to a PolyLoop object in gbXML format.
        /// </summary>
        /// <param name="closedPlanar3D">The IClosedPlanar3D object to convert.</param>
        /// <param name="tolerance">The tolerance to use in the conversion process. Defaults to Core.Tolerance.MicroDistance.</param>
        /// <returns>A PolyLoop object in gbXML format representing the IClosedPlanar3D object.</returns>
        public static PolyLoop TogbXML_PolyLoop(this IClosedPlanar3D closedPlanar3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (closedPlanar3D == null)
                return null;

            List<Point3D> point3Ds = null;

            IClosedPlanar3D closedPlanar3D_Temp = null;
            if (closedPlanar3D is Face3D)
                closedPlanar3D_Temp = ((Face3D)closedPlanar3D).GetExternalEdge3D();

            if (closedPlanar3D_Temp == null)
                closedPlanar3D_Temp = closedPlanar3D;

            if (closedPlanar3D_Temp is ISegmentable3D)
                point3Ds = ((ISegmentable3D)closedPlanar3D_Temp).GetPoints();

            if (point3Ds == null)
                throw new System.NotImplementedException("Conversion to gbXML format is not implemented for the given geometry object.");

            bool? clockwise = Spatial.Query.Clockwise(closedPlanar3D_Temp, null, tolerance);
            if (clockwise != null && clockwise.HasValue)
            {
                if (!clockwise.Value)
                    point3Ds.Reverse();
            }

            //Plane plane = Spatial.Create.Plane(point3Ds, tolerance);
            //if (!plane.Normal.SameHalf(closedPlanar3D.GetPlane().Normal))
            //    point3Ds.Reverse();


            PolyLoop polyLoop = new PolyLoop();
            polyLoop.Points = point3Ds.ConvertAll(x => x.TogbXML(tolerance)).ToArray();

            return polyLoop;
        }

    }
}
