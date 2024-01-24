namespace TheNextBigThing.Domain.Exceptions;

public class CurrencyDataException : Exception
{
    public CurrencyDataException(string message) : base(message) { }
}
