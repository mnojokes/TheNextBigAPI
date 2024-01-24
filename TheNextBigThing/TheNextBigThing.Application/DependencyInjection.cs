using Microsoft.Extensions.DependencyInjection;
using TheNextBigThing.Application.Clients;
using TheNextBigThing.Application.Services;

namespace TheNextBigThing.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<LBCurrencyClient>();
        services.AddTransient<CurrencyRateService>();
        services.AddHostedService<PeriodicHostedService>();
    }
}
