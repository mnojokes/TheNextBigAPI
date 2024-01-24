using System.Data;
using TheNextBigThing.Domain.Entities;
using TheNextBigThing.Domain.Interfaces;

namespace TheNextBigThing.Infrastructure.Repositories;

public class CurrencyRateRepository : ICurrencyRateRepository
{
    private readonly IDbConnection _connection;

    public CurrencyRateRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<CurrencyDataEntity?> Get(DateTime date)
    {
        throw new NotImplementedException();
    }

    public async Task Store(CurrencyDataEntity data)
    {
        throw new NotImplementedException();
    }

    public Task CleanUp()
    {
        throw new NotImplementedException();
    }
}
