using gbXMLSerializer;
using SAM.Analytical;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static RectangularGeometry TogbXML_RectangularGeometry(this PlanarBoundary3D planarBoundary3D, double tolerance = Core.Tolerance.MicroDistance)
        {
            if (planarBoundary3D == null)
                return null;

            return Geometry.gbXML.Convert.TogbXML_RectangularGeometry(planarBoundary3D.GetFace3D(), tolerance); ;
        }

    }
}
