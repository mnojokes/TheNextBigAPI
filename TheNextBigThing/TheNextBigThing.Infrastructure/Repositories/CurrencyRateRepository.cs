using TheNextBigThing.Domain.Entities;
using TheNextBigThing.Domain.Interfaces;

namespace TheNextBigThing.Infrastructure.Repositories;

public class CurrencyRateRepository : ICurrencyRateRepository
{
    public async Task<CurrencyDataEntity?> Get(DateTime date)
    {
        throw new NotImplementedException();
    }

    public async Task Store(CurrencyDataEntity data)
    {
        throw new NotImplementedException();
    }
}
