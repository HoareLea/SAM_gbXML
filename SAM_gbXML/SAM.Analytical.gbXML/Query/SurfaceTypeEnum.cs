using gbXMLSerializer;
using SAM.Geometry.Spatial;
using System;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static surfaceTypeEnum SurfaceTypeEnum(this PanelType panelType)
        {
            if (panelType == Analytical.PanelType.Undefined)
                return surfaceTypeEnum.Air;

            switch (panelType)
            {
                case Analytical.PanelType.Ceiling:
                    return surfaceTypeEnum.Ceiling;
                case Analytical.PanelType.CurtainWall:
                    return surfaceTypeEnum.ExteriorWall;
                case Analytical.PanelType.Floor:
                    return surfaceTypeEnum.SlabOnGrade;
                case Analytical.PanelType.FloorExposed:
                    return surfaceTypeEnum.ExposedFloor;
                case Analytical.PanelType.FloorInternal:
                    return surfaceTypeEnum.InteriorFloor;
                case Analytical.PanelType.FloorRaised:
                    return surfaceTypeEnum.RaisedFloor;
                case Analytical.PanelType.Roof:
                    return surfaceTypeEnum.Roof;
                case Analytical.PanelType.Shade:
                    return surfaceTypeEnum.Shade;
                case Analytical.PanelType.SlabOnGrade:
                    return surfaceTypeEnum.SlabOnGrade;
                case Analytical.PanelType.SolarPanel:
                    return surfaceTypeEnum.Shade;
                case Analytical.PanelType.UndergroundCeiling:
                    return surfaceTypeEnum.UndergroundCeiling;
                case Analytical.PanelType.UndergroundSlab:
                    return surfaceTypeEnum.UndergroundSlab;
                case Analytical.PanelType.UndergroundWall:
                    return surfaceTypeEnum.UndergroundWall; ;
                case Analytical.PanelType.Wall:
                    return surfaceTypeEnum.ExteriorWall;
                case Analytical.PanelType.WallExternal:
                    return surfaceTypeEnum.ExteriorWall;
                case Analytical.PanelType.WallInternal:
                    return surfaceTypeEnum.InteriorWall;
            }

            return surfaceTypeEnum.Air;
        }
    }
}