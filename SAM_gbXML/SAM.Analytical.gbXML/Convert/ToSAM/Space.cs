// This file contains a static class that provides a conversion method for gbXMLSerializer.Space objects to SAM.Analytical.Space objects
// The method name is ToSAM() and takes in a gbXMLSerializer.Space object as well as two optional double parameters for silverSpacing and tolerance
// The returned value is a SAM.Analytical.Space object that contains the converted gbXML space data
// If the input space object is null, then null is returned
// The method utilizes the SAM.Geometry.Spatial and SAM.Analytical namespaces
// The location of the converted space is obtained using the Query.Location() method with the specified silverSpacing and tolerance values
// The method sets the space name and id parameters of the returned SAM.Core.Space object based on the corresponding properties of the input gbXML space object.
using SAM.Core;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts gbXMLSerializer.Space objects to SAM.Core.Space objects.
        /// </summary>
        /// <param name="space">The gbXML space object to convert.</param>
        /// <param name="silverSpacing">The silver spacing value to use for determining the space location.</param>
        /// <param name="tolerance">The tolerance value to use for determining the space location.</param>
        /// <returns>A SAM.Analytical.Space object containing the converted gbXML space data.</returns>
        public static Space ToSAM(this gbXMLSerializer.Space space, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.MicroDistance)
        {
            if (space == null)
                return null;

            Point3D location = Query.Location(space.ShellGeo, silverSpacing, tolerance);

            Space result = new Space(space.Name, location);
            result.SetValue(SpaceParameter.Id, space.id);

            return result;
        }

    }
}
