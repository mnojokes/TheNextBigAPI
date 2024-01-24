using System.Xml.Serialization;

namespace TheNextBigThing.Domain.DTO;

[XmlRoot("RateChange")]
public class RateChange
{
    [XmlElement("Name")] public string Name { get; set; } = string.Empty;
    [XmlAttribute("Name")] public decimal Change { get; set; } = default;
}
