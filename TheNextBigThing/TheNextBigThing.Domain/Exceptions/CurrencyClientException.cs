namespace TheNextBigThing.Domain.Exceptions;

public class CurrencyClientException : Exception
{
    public CurrencyClientException(string message, int code) : base(message)
    {
        Code = code;
    }

    public int Code { get; }
}
