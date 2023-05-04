using gbXMLSerializer;
using SAM.Core;
using SAM.Geometry.gbXML;
using SAM.Geometry.Spatial;

// Defining a static class for converting gbXML objects to SAM analytical objects
namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a gbXML SpaceBoundary object to a SAM analytical IPartition object
        /// </summary>
        /// <param name="spaceBoundary">The SpaceBoundary object to convert</param>
        /// <param name="tolerance">The tolerance to use for the conversion (default is Tolerance.Distance)</param>
        /// <returns>The converted IPartition object</returns>
        public static IPartition ToSAM_Partition(this SpaceBoundary spaceBoundary, double tolerance = Tolerance.Distance)
        {
            if (spaceBoundary == null) // If the SpaceBoundary object is null, return null
                return null;

            // Convert the SpaceBoundary's planar geometry to a Polygon3D object using the given tolerance
            Polygon3D polygon3D = spaceBoundary.PlanarGeometry.ToSAM(tolerance);
            if (polygon3D == null) // If the Polygon3D object is null, return null
                return null;

            // Create a new IPartition object using the Polygon3D object
            return Analytical.Create.HostPartition(new Face3D(polygon3D));
        }

        /// <summary>
        /// Converts a gbXML Surface object to a SAM analytical IPartition object
        /// </summary>
        /// <param name="surface">The Surface object to convert</param>
        /// <param name="tolerance">The tolerance to use for the conversion (default is Tolerance.Distance)</param>
        /// <returns>The converted IPartition object</returns>
        public static IPartition ToSAM_Partition(this gbXMLSerializer.Surface surface, double tolerance = Tolerance.Distance)
        {
            // Convert the Surface's planar geometry to a Polygon3D object using the given tolerance
            Polygon3D polygon3D = surface?.PlanarGeometry?.ToSAM(tolerance);
            if (polygon3D == null) // If the Polygon3D object is null, return null
            {
                return null;
            }

            // Create a new Face3D object using the Polygon3D object
            Face3D face3D = new(polygon3D);
            if (face3D == null) // If the Face3D object is null, return null
            {
                return null;
            }

            IPartition result = null;
            switch (surface.surfaceType) // Check the surface type
            {
                case surfaceTypeEnum.Air: // If the surface type is Air, create a new AirPartition object using the Face3D object
                    result = new AirPartition(face3D);
                    break;

                case surfaceTypeEnum.Ceiling: // If the surface type is Ceiling, create a new HostPartition object with a FloorType object and the Face3D object
                    FloorType floorType = new(surface.Name);
                    result = Analytical.Create.HostPartition(face3D, floorType, tolerance);
                    break;
            }

            // If the result is a HostPartition, add any openings to it
            if (result is IHostPartition hostPartition)
            {
                //IHostPartition hostPartition = (IHostPartition)result;
                Opening[] openings_gbXML = surface.Opening;
                if (openings_gbXML != null)
                {
                    foreach (Opening opening_gbXML in openings_gbXML)
                    {
                        IOpening opening = opening_gbXML.ToSAM_Opening(tolerance);
                        if (opening != null)
                            hostPartition.AddOpening(opening);
                    }
                }
            }

            return result;
        }

    }
}
