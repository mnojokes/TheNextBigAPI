namespace TheNextBigThing.Domain.Entities;

public class CurrencyDataEntity
{
    public DateTime Date { get; set; } = default;
    public string Rates { get; set; } = string.Empty;
}
