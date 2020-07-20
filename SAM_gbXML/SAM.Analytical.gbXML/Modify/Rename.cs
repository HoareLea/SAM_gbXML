using gbXMLSerializer;

namespace SAM.Analytical.gbXML
{
    public static partial class Modify
    {
        public static bool Rename(this gbXMLSerializer.Space space, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            space.Name = name;
            return true;
        }
    }
}