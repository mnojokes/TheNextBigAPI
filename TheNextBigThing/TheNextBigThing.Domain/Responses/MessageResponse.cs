using System.Xml.Serialization;

namespace TheNextBigThing.Domain.Responses;

[XmlRoot("MessageResponse")]
public class MessageResponse
{
    [XmlElement("Message")] public string Message { get; set; } = string.Empty;
}
