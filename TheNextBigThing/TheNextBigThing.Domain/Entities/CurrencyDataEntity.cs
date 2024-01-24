namespace TheNextBigThing.Domain.Entities;

public class CurrencyDataEntity
{
    public DateTime Date { get; set; } = default;
    public string RatesXml { get; set; } = string.Empty;
}
