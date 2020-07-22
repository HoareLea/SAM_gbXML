using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static CADObjectId CADObjectId(this Panel panel, int cADObjectIdSufix = -1)
        {
            if (panel == null)
                return null;

            string name = null;
            switch (panel.PanelType)
            {
                case Analytical.PanelType.Ceiling:
                    name = "Compound Ceiling";
                    break;
                case Analytical.PanelType.CurtainWall:
                    name = "Curtain Wall";
                    break;
                case Analytical.PanelType.Floor:
                case Analytical.PanelType.FloorExposed:
                case Analytical.PanelType.FloorInternal:
                case Analytical.PanelType.FloorRaised:
                case Analytical.PanelType.SlabOnGrade:
                case Analytical.PanelType.UndergroundSlab:
                case Analytical.PanelType.UndergroundCeiling:
                    name = "Floor";
                    break;
                case Analytical.PanelType.Roof:
                case Analytical.PanelType.Shade:
                case Analytical.PanelType.SolarPanel:
                    name = "Basic Roof";
                    break;
                case Analytical.PanelType.UndergroundWall:
                case Analytical.PanelType.Wall:
                case Analytical.PanelType.WallExternal:
                case Analytical.PanelType.WallInternal:
                    name = "Basic Wall";
                    break;
                case Analytical.PanelType.Air:
                    name = "Air";
                    break;
                default:
                    name = "Undefined";
                    break;
            }

            name += ":";

            string panelName = panel.Name;
            if (string.IsNullOrWhiteSpace(panelName))
                panelName = "Default";

            name += string.Format(" {0}", panelName);

            if (cADObjectIdSufix != -1)
                name += string.Format(" [{0}]", cADObjectIdSufix);

            if (name.EndsWith(":"))
                name = name.Substring(0, name.Length - 1);

            CADObjectId cADObjectId = new CADObjectId();
            cADObjectId.id = name;

            return cADObjectId;
        }

        public static CADObjectId CADObjectId(this Aperture aperture, int cADObjectIdSufix = -1)
        {
            if (aperture == null)
                return null;

            ApertureConstruction apertureConstruction = aperture.ApertureConstruction;

            ApertureType apertureType = Analytical.ApertureType.Undefined;
            if(apertureConstruction != null)
                apertureType = apertureConstruction.ApertureType;

            string name = null;

            switch (apertureType)
            {
                case Analytical.ApertureType.Window:
                    name = "Window";
                    break;
                case Analytical.ApertureType.Door:
                    name = "Door";
                    break;
                case Analytical.ApertureType.Undefined:
                    name = "Undefined";
                    break;
            }

            name += ":";

            string apertureName = aperture.Name;
            if (string.IsNullOrWhiteSpace(apertureName) && apertureConstruction != null)
                apertureName = apertureConstruction.Name;

            if (!string.IsNullOrWhiteSpace(apertureName) && apertureName.Contains(":"))
                name = string.Empty;

            if (string.IsNullOrWhiteSpace(apertureName))
                apertureName = "Default";

            name += string.Format(" {0}", apertureName);

            name = name.Trim();

            if (cADObjectIdSufix != -1)
                name += string.Format(" [{0}]", cADObjectIdSufix);

            if (name.EndsWith(":"))
                name = name.Substring(0, name.Length - 1);

            CADObjectId cADObjectId = new CADObjectId();
            cADObjectId.id = name;

            return cADObjectId;
        }
    }
}