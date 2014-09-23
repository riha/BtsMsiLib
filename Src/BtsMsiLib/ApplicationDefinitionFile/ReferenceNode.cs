using System.Xml.Serialization;

namespace BtsMsiLib.ApplicationDefinitionFile
{
    [XmlRoot(ElementName = "Reference")]
    internal class ReferenceNode
    {
        internal string Name { get; set; }
    }
}
