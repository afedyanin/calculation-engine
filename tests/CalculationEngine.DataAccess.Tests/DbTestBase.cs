using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess.Tests;

public class DbTestBase : IDisposable
{
    private static readonly string _connectionString = "Server=localhost;Port=5432;Database=calculation_engine;User Id=postgres;Password=admin;";

    protected CalculationEngineDbContext Context { get; }

    public DbTestBase()
    {
        var builder = new DbContextOptionsBuilder<CalculationEngineDbContext>();
        builder.UseNpgsql(_connectionString);

        Context = new CalculationEngineDbContext(builder.Options);
    }

    public void Dispose()
    {
        Context.Dispose();
    }
}
