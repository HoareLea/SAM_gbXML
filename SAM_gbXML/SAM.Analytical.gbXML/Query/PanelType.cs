using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static PanelType PanelType(this surfaceTypeEnum surfaceTypeEnum)
        {
            switch(surfaceTypeEnum)
            {
                case surfaceTypeEnum.Air:
                    return Analytical.PanelType.Air;
                case surfaceTypeEnum.Ceiling:
                    return Analytical.PanelType.Ceiling;
                case surfaceTypeEnum.EmbeddedColumn:
                    return Analytical.PanelType.Shade;
                case surfaceTypeEnum.ExposedFloor:
                    return Analytical.PanelType.FloorExposed;
                case surfaceTypeEnum.ExteriorWall:
                    return Analytical.PanelType.WallExternal;
                case surfaceTypeEnum.FreestandingColumn:
                    return Analytical.PanelType.Shade;
                case surfaceTypeEnum.InteriorFloor:
                    return Analytical.PanelType.FloorInternal;
                case surfaceTypeEnum.InteriorWall:
                    return Analytical.PanelType.WallInternal;
                case surfaceTypeEnum.RaisedFloor:
                    return Analytical.PanelType.FloorRaised;
                case surfaceTypeEnum.Roof:
                    return Analytical.PanelType.Roof;
                case surfaceTypeEnum.Shade:
                    return Analytical.PanelType.Shade;
                case surfaceTypeEnum.SlabOnGrade:
                    return Analytical.PanelType.SlabOnGrade;
                case surfaceTypeEnum.UndergroundCeiling:
                    return Analytical.PanelType.UndergroundCeiling;
                case surfaceTypeEnum.UndergroundSlab:
                    return Analytical.PanelType.UndergroundSlab;
                case surfaceTypeEnum.UndergroundWall:
                    return Analytical.PanelType.UndergroundWall;
            }

            return Analytical.PanelType.Undefined;
        }
    }
}