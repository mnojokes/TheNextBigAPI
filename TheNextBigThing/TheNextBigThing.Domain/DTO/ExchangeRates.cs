using System.Xml.Serialization;

namespace TheNextBigThing.Domain.DTO;

[XmlRoot("ExchangeRates")]
public class ExchangeRates
{
    [XmlElement("item")]
    public List<CurrencyRate> Items { get; set; }
}
