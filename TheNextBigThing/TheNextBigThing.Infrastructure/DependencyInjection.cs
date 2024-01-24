using DbUp;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;
using System.Reflection;
using TheNextBigThing.Domain.Interfaces;
using TheNextBigThing.Infrastructure.Repositories;

namespace TheNextBigThing.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string? dbConnection)
    {
        if (string.IsNullOrEmpty(dbConnection))
        {
            throw new ArgumentNullException(nameof(dbConnection));
        }

        EnsureDatabase.For.PostgresqlDatabase(dbConnection);
        var result = DeployChanges.To
            .PostgresqlDatabase(dbConnection)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build()
            .PerformUpgrade();

        if (!result.Successful)
        {
            throw new InvalidOperationException("Error initializing Postgre database.");
        }

        services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(dbConnection));
        services.AddTransient<ICurrencyRateRepository, CurrencyRateRepository>();
    }
}
