using System.Xml.Serialization;

namespace TheNextBigThing.Domain.DTO;

[XmlRoot("CurrencyRate")]
public class CurrencyRate
{
    [XmlElement("date")] public string Date { get; set; } = string.Empty;
    [XmlElement("currency")] public string Currency { get; set; } = string.Empty;
    [XmlElement("quantity")] public int Quantity { get; set; } = default;
    [XmlElement("rate")] public decimal Rate { get; set; } = default;
    [XmlElement("unit")] public string Unit { get; set; } = string.Empty;
}
