using System.Net;
using TheNextBigThing.Domain.Exceptions;

namespace TheNextBigThing.Application.Clients;

public class LBCurrencyClient
{
    private readonly HttpClient _client;
    private readonly string _host = "https://www.lb.lt/webservices/ExchangeRates/ExchangeRates.asmx";
    public LBCurrencyClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> Get(DateTime date)
    {
        string request = $"{_host}/getExchangeRatesByDate?Date={date:yyyy-MM-dd}";
        HttpResponseMessage response = await _client.GetAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new CurrencyClientException("Unable to retrieve rates: server error.", (int)response.StatusCode);
        }

        string responseStr = await response.Content.ReadAsStringAsync();
        if (responseStr.Contains("<message>"))
        {
            throw new CurrencyClientException($"Unable to retrieve rates for {date:yyyy-MM-dd}", (int)HttpStatusCode.BadRequest);
        }

        return responseStr;
    }
}
