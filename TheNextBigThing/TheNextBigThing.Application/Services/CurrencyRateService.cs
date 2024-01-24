using TheNextBigThing.Application.Clients;
using TheNextBigThing.Domain.Entities;
using TheNextBigThing.Domain.Interfaces;
using TheNextBigThing.Domain.Responses;

namespace TheNextBigThing.Application.Services;

public class CurrencyRateService
{
    private readonly LBCurrencyClient _client;
    private readonly ICurrencyRateRepository _rateRepository;

    public CurrencyRateService(LBCurrencyClient client, ICurrencyRateRepository currencyRateRepository)
    {
        _client = client;
        _rateRepository = currencyRateRepository;
    }

    public async Task<RateChangesResponse> GetRateChanges(DateTime date)
    {
        CurrencyDataEntity? current = await _rateRepository.Get(date);
        if (current is null)
        {
            current = await Download(date);
            await _rateRepository.Store(current);
        }

        CurrencyDataEntity? previous = await _rateRepository.Get(date);
        if (previous is null)
        {
            previous = await Download(new DateTime(date.Year, date.Month, date.Day));
            await _rateRepository.Store(previous);
        }

        throw new NotImplementedException();
    }

    private async Task<CurrencyDataEntity> Download(DateTime date)
    {
        return new CurrencyDataEntity()
        {
            Date = date,
            RatesXml = await _client.Get(date)
        };
    }
}
