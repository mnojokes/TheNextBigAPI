using System.Xml.Serialization;
using TheNextBigThing.Domain.DTO;

namespace TheNextBigThing.Domain.Responses;

[XmlRoot("RateChangesResponse")]
public class RateChangesResponse
{
    [XmlElement("Date")] public DateTime Date { get; set; } = default;
    [XmlArray("Changes")] public List<RateChange> Changes { get; set; }
}
