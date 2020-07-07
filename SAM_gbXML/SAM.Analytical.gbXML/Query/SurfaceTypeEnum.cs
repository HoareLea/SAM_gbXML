using gbXMLSerializer;
using SAM.Geometry.Spatial;
using System;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static surfaceTypeEnum SurfaceTypeEnum(this PanelType panelType)
        {
            if (panelType == PanelType.Undefined)
                return surfaceTypeEnum.Air;

            switch (panelType)
            {
                case PanelType.Ceiling:
                    return surfaceTypeEnum.Ceiling;
                case PanelType.CurtainWall:
                    return surfaceTypeEnum.ExteriorWall;
                case PanelType.Floor:
                    return surfaceTypeEnum.SlabOnGrade;
                case PanelType.FloorExposed:
                    return surfaceTypeEnum.ExposedFloor;
                case PanelType.FloorInternal:
                    return surfaceTypeEnum.InteriorFloor;
                case PanelType.FloorRaised:
                    return surfaceTypeEnum.RaisedFloor;
                case PanelType.Roof:
                    return surfaceTypeEnum.Roof;
                case PanelType.Shade:
                    return surfaceTypeEnum.Shade;
                case PanelType.SlabOnGrade:
                    return surfaceTypeEnum.SlabOnGrade;
                case PanelType.SolarPanel:
                    return surfaceTypeEnum.Shade;
                case PanelType.UndergroundCeiling:
                    return surfaceTypeEnum.UndergroundCeiling;
                case PanelType.UndergroundSlab:
                    return surfaceTypeEnum.UndergroundSlab;
                case PanelType.UndergroundWall:
                    return surfaceTypeEnum.UndergroundWall; ;
                case PanelType.Wall:
                    return surfaceTypeEnum.ExteriorWall;
                case PanelType.WallExternal:
                    return surfaceTypeEnum.ExteriorWall;
                case PanelType.WallInternal:
                    return surfaceTypeEnum.InteriorWall;
            }

            return surfaceTypeEnum.Air;
        }
    }
}