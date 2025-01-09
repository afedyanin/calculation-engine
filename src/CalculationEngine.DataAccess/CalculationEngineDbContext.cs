using CalculationEngine.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess;

public class CalculationEngineDbContext : DbContext
{
    public CalculationEngineDbContext(DbContextOptions<CalculationEngineDbContext> options) : base(options)
    {
    }

    public DbSet<CalculationResultItem> CalculationResultItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalculationResultItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calculation_result_item_pkey");

            entity.ToTable("calculation_result_item");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CalculationUnitId)
                .IsRequired()
                .HasColumnName("calculation_unit_id");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.Metadata).HasColumnName("metadata");

            entity.Property(e => e.PayloadJson)
                .HasColumnType("jsonb")
                .HasColumnName("payload_json");
        });
    }
}
