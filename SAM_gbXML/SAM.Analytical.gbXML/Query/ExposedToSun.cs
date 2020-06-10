using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static bool ExposedToSun(this PanelType panelType)
        {
            if (panelType == PanelType.Undefined)
                return false;

            switch(panelType)
            {
                case PanelType.CurtainWall:
                case PanelType.FloorExposed:
                case PanelType.Roof:
                case PanelType.Shade:
                case PanelType.SolarPanel:
                case PanelType.WallExternal:
                    return true;
            }

            return false;
        }
    }
}