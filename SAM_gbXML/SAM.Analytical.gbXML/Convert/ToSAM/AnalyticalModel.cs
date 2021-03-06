﻿using gbXMLSerializer;
using SAM.Core;
using SAM.Core.gbXML;
using System.Collections.Generic;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        public static AnalyticalModel ToSAM(this gbXMLSerializer.gbXML gbXML, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.MicroDistance)
        {
            if (gbXML == null)
                return null;

            return ToSAM(gbXML.Campus, silverSpacing, tolerance);
        }

        public static AnalyticalModel ToSAM(this Campus campus, double silverSpacing = Tolerance.MacroDistance, double tolerance = Tolerance.MicroDistance)
        {
            Address address = campus.Location.ToSAM_Address();
            Core.Location location = campus.Location.ToSAM(tolerance);

            AdjacencyCluster adjacencyCluster = new AdjacencyCluster();

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

                            adjacencyCluster.AddObject(space_SAM);
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
                    
                    Panel panel = surface.ToSAM(tolerance);
                    if (panel == null)
                        continue;

                    adjacencyCluster.AddObject(panel);

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

                        adjacencyCluster.AddRelation(panel, space);
                    }

                }
            }

            AnalyticalModel result = new AnalyticalModel(string.Empty, string.Empty, location, address, adjacencyCluster);

            return result;
        }

    }
}
