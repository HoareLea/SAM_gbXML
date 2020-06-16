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
                case PanelType.Ceiling:
                    name = "Compound Ceiling";
                    break;
                case PanelType.CurtainWall:
                    name = "Curtain Wall";
                    break;
                case PanelType.Floor:
                case PanelType.FloorExposed:
                case PanelType.FloorInternal:
                case PanelType.FloorRaised:
                case PanelType.SlabOnGrade:
                case PanelType.UndergroundSlab:
                case PanelType.UndergroundCeiling:
                    name = "Floor";
                    break;
                case PanelType.Roof:
                case PanelType.Shade:
                case PanelType.SolarPanel:
                    name = "Basic Roof";
                    break;
                case PanelType.UndergroundWall:
                case PanelType.Wall:
                case PanelType.WallExternal:
                case PanelType.WallInternal:
                    name = "Basic Wall";
                    break;
                case PanelType.Air:
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

            string name = aperture.Name;
            if (string.IsNullOrWhiteSpace(name) && apertureConstruction != null)
                name = apertureConstruction.Name;

            if (string.IsNullOrWhiteSpace(name))
                return null;

            CADObjectId cADObjectId = new CADObjectId();
            if (cADObjectIdSufix == -1)
                cADObjectId.id = string.Format("{0}", name);
            else
                cADObjectId.id = string.Format("{0} [{1}]", name, cADObjectIdSufix);

            return cADObjectId;

            //string prefix = null;
            //if(apertureConstruction != null)
            //{
            //    switch (aperture.ApertureConstruction.ApertureType)
            //    {
            //        case ApertureType.Window:
            //            prefix = "Window";
            //            break;
            //        case ApertureType.Door:
            //            prefix = "Door";
            //            break;
            //    }
            //}

            //CADObjectId cADObjectId = new CADObjectId();
            //if (cADObjectIdSufix == -1)
            //    cADObjectId.id = string.Format("{0}: {1}", prefix, name);
            //else
            //    cADObjectId.id = string.Format("{0}: {1} [{2}]", prefix, name, cADObjectIdSufix);

            //return cADObjectId;
        }
    }
}