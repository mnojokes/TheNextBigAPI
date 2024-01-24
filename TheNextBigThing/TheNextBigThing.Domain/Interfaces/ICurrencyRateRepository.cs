using TheNextBigThing.Domain.Entities;

namespace TheNextBigThing.Domain.Interfaces;

public interface ICurrencyRateRepository
{
    public Task<CurrencyDataEntity?> Get(DateTime date);
    public Task Store(CurrencyDataEntity data);
    public Task CleanUp();
}
