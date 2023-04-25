using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Static class containing extension methods for converting gbXML geometry objects to SAM geometry objects
    /// </summary>
    public static partial class Convert
    {
        /// <summary>
        /// Convert gbXML opening object to SAM aperture object
        /// </summary>
        /// <param name="opening">gbXML opening object</param>
        /// <param name="tolerance">Tolerance for geometry comparison</param>
        /// <returns>SAM aperture object</returns>
        public static Aperture ToSAM(this gbXMLSerializer.Opening opening, double tolerance = Tolerance.MicroDistance)
        {
            // Check if the input opening object is null
            if (opening == null)
                return null;

            // Convert gbXML polygon to SAM polygon
            Polygon3D polygon3D = opening.pg.ToSAM(tolerance);

            // Check if the converted polygon is null
            if (polygon3D == null)
            {
                return null;
            }

            // Get the SAM aperture type based on the gbXML opening type
            ApertureType apertureType = Query.ApertureType(opening.openingType);

            // Create an SAM aperture construction object based on the gbXML opening name and SAM aperture type
            ApertureConstruction apertureConstruction = new ApertureConstruction(opening.Name, apertureType);

            // Create a new SAM aperture object with the aperture construction, polygon and location
            Aperture result = new Aperture(apertureConstruction, polygon3D, Analytical.Query.OpeningLocation(polygon3D, tolerance));

            // Return the SAM aperture object
            return result;
        }
    }
}


