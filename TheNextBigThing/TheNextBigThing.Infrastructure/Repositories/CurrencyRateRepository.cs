using Dapper;
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
        string sql = "SELECT * FROM \"exchange_rates\" WHERE \"date\" = @date;";
        var queryParameters = new
        {
            date = new DateTime(date.Year, date.Month, date.Day),
        };

        return await _connection.QuerySingleOrDefaultAsync<CurrencyDataEntity>(sql, queryParameters);
    }

    public async Task Store(CurrencyDataEntity data)
    {
        string sql = "INSERT INTO \"exchange_rates\" (\"date\", \"rates\") VALUES (@date, @rates);";
        var queryParameters = new
        {
            date = new DateTime(data.Date.Year, data.Date.Month, data.Date.Day),
            rates = data.Rates
        };

        int linesAdded = await _connection.ExecuteAsync(sql, queryParameters);
        if (linesAdded != 1)
        {
            // Log error, do not break
            await Console.Out.WriteLineAsync("Error writing to cache DB");
        }
    }

    public async Task CleanUp()
    {
        string sql = "DELETE FROM \"exchange_rates\";";
        await _connection.ExecuteAsync(sql);
    }
}
