using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CalculationEngine.DataAccess;

public class CalculationEngineDbContextFactory : IDesignTimeDbContextFactory<CalculationEngineDbContext>
{
    public CalculationEngineDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CalculationEngineDbContext>();

        optionsBuilder.UseNpgsql<CalculationEngineDbContext>("Server=localhost;Port=5432;Database=calculation_engine;User Id=postgres;Password=admin;");

        return new CalculationEngineDbContext(optionsBuilder.Options);
    }
}
