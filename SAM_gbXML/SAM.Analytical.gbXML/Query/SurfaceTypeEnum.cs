using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// This class contains extension methods that provide conversion between Analytical objects and their corresponding gbXML objects.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Converts a <see cref="PanelType"/> to a <see cref="surfaceTypeEnum"/>.
        /// </summary>
        /// <param name="panelType">The <see cref="PanelType"/> to be converted.</param>
        /// <returns>The corresponding <see cref="surfaceTypeEnum"/>.</returns>
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
