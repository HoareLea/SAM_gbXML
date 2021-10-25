using gbXMLSerializer;
using SAM.Core;
using SAM.Core.gbXML;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static ArchitecturalModel ToSAM_ArchitecturalModel(this gbXMLSerializer.gbXML gbXML, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.Distance)
        {
            if (gbXML == null)
                return null;

            return ToSAM_ArchitecturalModel(gbXML.Campus, silverSpacing, tolerance);
        }

        public static ArchitecturalModel ToSAM_ArchitecturalModel(this Campus campus, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.Distance)
        {
            Address address = campus.Location.ToSAM_Address();
            Core.Location location = campus.Location.ToSAM(tolerance);

            ArchitecturalModel result = new ArchitecturalModel(null, location, address, Architectural.Create.PlanarTerrain(0), new MaterialLibrary(string.Empty), new ProfileLibrary(string.Empty));

            Dictionary<string, Space> dictionary_Space = new Dictionary<string, Space>();
            Building[] buildings = campus.Buildings;
            if(buildings != null)
            {
                foreach (Building building in buildings)
                {
                    gbXMLSerializer.Space[] spaces = building.Spaces;
                    if(spaces != null)
                    {
                        foreach (gbXMLSerializer.Space space_gbXML in spaces)
                        {
                            if (space_gbXML == null)
                                continue;

                            Space space_SAM = space_gbXML.ToSAM(silverSpacing, tolerance);
                            if (space_SAM == null)
                                continue;

                            dictionary_Space[space_gbXML.id] = space_SAM;

                            result.Add(space_SAM);
                        }
                    }
                }
            }

            Surface[] surfaces = campus.Surface;
            if(surfaces != null)
            {
                foreach (Surface surface in surfaces)
                {
                    if (surface == null)
                        continue;
                    
                    IPartition partition = surface.ToSAM_Partition(tolerance);
                    if (partition == null)
                        continue;

                    result.Add(partition);

                    AdjacentSpaceId[] adjacentSpaceIds = surface.AdjacentSpaceId;
                    if (adjacentSpaceIds == null || adjacentSpaceIds.Length == 0)
                        continue;

                    foreach(AdjacentSpaceId adjacentSpaceId in adjacentSpaceIds)
                    {
                        string id = adjacentSpaceId?.spaceIdRef;
                        if (string.IsNullOrWhiteSpace(id))
                            continue;

                        Space space = null;

                        if (!dictionary_Space.TryGetValue(id, out space))
                            continue;

                        result.AddRelation(partition, space);
                    }

                }
            }

            return result;
        }

    }
}
