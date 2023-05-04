using SAM.Core;

namespace SAM.Analytical.gbXML
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts gbXML material library to SAM material library
        /// </summary>
        /// <param name="gbXML">gbXML object containing material library</param>
        /// <returns>SAM material library</returns>
        public static MaterialLibrary ToSAM_MaterialLibrary(this gbXMLSerializer.gbXML gbXML)
        {
            if (gbXML == null)
            {
                return null;
            }

            gbXMLSerializer.Material[] materials = gbXML.Materials;
            if(materials == null)
            {
                return null;
            }

            MaterialLibrary result = new MaterialLibrary(string.Empty);
            foreach(gbXMLSerializer.Material material_gbXML in materials)
            {
                Material material = ToSAM(material_gbXML);
                if(material != null)
                {
                    result.Add(material);
                }
            }

            return result;
        }

    }
}
