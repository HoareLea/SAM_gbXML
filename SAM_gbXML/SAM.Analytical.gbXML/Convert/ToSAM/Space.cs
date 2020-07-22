using SAM.Core;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static Space ToSAM(this gbXMLSerializer.Space space, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.MicroDistance)
        {
            if (space == null)
                return null;

            Point3D location = Query.Location(space.ShellGeo, silverSpacing, tolerance);

            Space result = new Space(space.Name, location);

            return result;
        }

    }
}
