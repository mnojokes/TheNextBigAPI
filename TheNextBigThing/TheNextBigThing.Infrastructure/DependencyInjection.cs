using Microsoft.Extensions.DependencyInjection;
using TheNextBigThing.Domain.Interfaces;
using TheNextBigThing.Infrastructure.Repositories;

namespace TheNextBigThing.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<ICurrencyRateRepository, CurrencyRateRepository>();
    }
}
