using CalculationEngine.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess;

public class CalculationEngineDbContext : DbContext
{
    public CalculationEngineDbContext(DbContextOptions<CalculationEngineDbContext> options) : base(options)
    {
    }

    public DbSet<CalculationResultItem> CalculationResultItems { get; set; }

    public DbSet<CalculationUnit> CalculationUnits { get; set; }

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

        modelBuilder.Entity<CalculationUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("calculation_unit_pkey");

            entity.ToTable("calculation_unit");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.GraphId)
                .IsRequired()
                .HasColumnName("graph_id");

            entity.Property(e => e.JobId).HasColumnName("job_id");

            // TODO?
            //entity.Property(e => e.Request).HasColumnName("request_json");
            //entity.Property(e => e.CalculationResults).HasColumnName("calculation_results");
        });
    }
}
