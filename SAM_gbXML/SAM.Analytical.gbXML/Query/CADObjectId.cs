using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Query
    {
        public static CADObjectId CADObjectId(this Panel panel, int cADObjectIdSufix = -1)
        {
            if (panel == null)
                return null;

            string prefix = null;
            switch(panel.PanelType)
            {
                case PanelType.Ceiling:
                    prefix = "Compound Ceiling";
                    break;
                case PanelType.CurtainWall:
                    prefix = "Curtain Wall";
                    break;
                case PanelType.Floor:
                case PanelType.FloorExposed:
                case PanelType.FloorInternal:
                case PanelType.FloorRaised:
                case PanelType.SlabOnGrade:
                case PanelType.UndergroundSlab:
                case PanelType.UndergroundCeiling:
                    prefix = "Floor";
                    break;
                case PanelType.Roof:
                case PanelType.Shade:
                case PanelType.SolarPanel:
                    prefix = "Basic Roof";
                    break;
                case PanelType.UndergroundWall:
                case PanelType.Wall:
                case PanelType.WallExternal:
                case PanelType.WallInternal:
                    prefix = "Basic Wall";
                    break;
                default:
                    prefix = "Undefined";
                    break;
            }

            CADObjectId cADObjectId = new CADObjectId();
            if (cADObjectIdSufix == -1)
                cADObjectId.id = string.Format("{0}: {1}", prefix, panel.Name);
            else
                cADObjectId.id = string.Format("{0}: {1} [{2}]", prefix, panel.Name, cADObjectIdSufix);
            
                return cADObjectId;
        }

        public static CADObjectId CADObjectId(this Aperture aperture, int cADObjectIdSufix = -1)
        {
            if (aperture == null)
                return null;

            ApertureConstruction apertureConstruction = aperture.ApertureConstruction;

            string name = aperture.Name;
            if (string.IsNullOrWhiteSpace(name) && apertureConstruction != null)
                name = apertureConstruction.Name;

            if (string.IsNullOrWhiteSpace(name))
                return null;

            string prefix = null;
            if(apertureConstruction != null)
            {
                switch (aperture.ApertureConstruction.ApertureType)
                {
                    case ApertureType.Window:
                        prefix = "Window";
                        break;
                    case ApertureType.Door:
                        prefix = "Door";
                        break;
                }
            }


            CADObjectId cADObjectId = new CADObjectId();
            if (cADObjectIdSufix == -1)
                cADObjectId.id = string.Format("{0}: {1}", prefix, name);
            else
                cADObjectId.id = string.Format("{0}: {1} [{2}]", prefix, name, cADObjectIdSufix);

            return cADObjectId;
        }
    }
}