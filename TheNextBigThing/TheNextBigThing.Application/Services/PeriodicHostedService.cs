using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TheNextBigThing.Domain.Interfaces;

namespace TheNextBigThing.Application.Services;

public class PeriodicHostedService : BackgroundService
{
    private readonly TimeSpan _period;
    private readonly ICurrencyRateRepository _repository;

    public PeriodicHostedService(ICurrencyRateRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        double cleanupPeriod;
        if (!double.TryParse(configuration["PeriodicCleanupHrs"], out cleanupPeriod))
        {
            throw new ArgumentNullException("PeriodicCleanupHrs");
        }
        _period = TimeSpan.FromHours(cleanupPeriod);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await _repository.CleanUp();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"{ex.GetType()} caught: {ex.Message}");
            }
        }
    }
}
