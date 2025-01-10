using CalculationEngine.Core.Model;
using CalculationEngine.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculationEngine.DataAccess;

public class CalculationEngineDbContext : DbContext
{
    public CalculationEngineDbContext(DbContextOptions<CalculationEngineDbContext> options) : base(options)
    {
    }

    public DbSet<CalculationResultItem> CalculationResultItems { get; set; }

    public DbSet<CalculationUnit> CalculationUnits { get; set; }

    internal DbSet<GraphDbo> Graphs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalculationResultItem>(entity =>
        {
            entity.ToTable("calculation_result_item");

            entity.HasKey(e => e.Id)
                .HasName("calculation_result_item_pkey");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.CalculationUnitId)
                .IsRequired()
                .HasColumnName("calculation_unit_id");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.Property(e => e.ContentType)
                .HasField("_contentType")
                .HasColumnName("content_type");

            entity.Property(e => e.ContentJson)
                .HasField("_contentJson")
                .HasColumnName("content_json")
                .HasColumnType("jsonb");

            entity.Ignore(e => e.Content);

            entity.HasOne(e => e.CalculationUnit)
                .WithMany(e => e.Results)
                .HasForeignKey(e => e.CalculationUnitId)
                .IsRequired();
        });

        modelBuilder.Entity<CalculationUnit>(entity =>
        {
            entity.ToTable("calculation_unit");

            entity.HasKey(e => e.Id)
                .HasName("calculation_unit_pkey");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.GraphId)
                .IsRequired()
                .HasColumnName("graph_id");

            entity.Property(e => e.JobId)
                .HasColumnName("job_id");

            entity.Property("_requestType")
                .HasColumnName("request_type");

            entity.Property("_requestJson")
                .HasColumnName("request_json")
                .HasColumnType("jsonb");

            entity.Ignore(e => e.Request);

            entity.HasMany(e => e.Results)
                .WithOne(e => e.CalculationUnit)
                .HasForeignKey(e => e.CalculationUnitId)
                .IsRequired();
        });

        modelBuilder.Entity<GraphDbo>(entity =>
        {
            entity.ToTable("calculation_graph");

            entity.HasKey(e => e.Id)
                .HasName("calculation_graph_pkey");

            entity.Property(e => e.Id)
                .HasColumnName("id");

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            entity.OwnsMany(e => e.Vertices,
                vertexBuilder =>
                {
                    vertexBuilder.ToJson("vertices_json");
                });
        });

    }
}
