using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    /// <summary>
    /// Provides methods for querying gbXML objects.
    /// </summary>
    public static partial class Query
    {
        /// <summary>
        /// Returns the corresponding <see cref="Analytical.PanelType"/> for the specified <see cref="surfaceTypeEnum"/>.
        /// </summary>
        /// <param name="surfaceTypeEnum">The surface type.</param>
        /// <returns>The corresponding <see cref="Analytical.PanelType"/>.</returns>
        public static PanelType PanelType(this surfaceTypeEnum surfaceTypeEnum)
        {
            switch (surfaceTypeEnum)
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
