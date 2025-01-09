﻿// <auto-generated />
using System;
using CalculationEngine.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CalculationEngine.DataAccess.Migrations
{
    [DbContext(typeof(CalculationEngineDbContext))]
    partial class CalculationEngineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CalculationEngine.Core.Model.CalculationResultItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("CalculationUnitId")
                        .HasColumnType("uuid")
                        .HasColumnName("calculation_unit_id");

                    b.Property<string>("ContentJson")
                        .HasColumnType("jsonb")
                        .HasColumnName("content_json");

                    b.Property<string>("ContentType")
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("calculation_result_item_pkey");

                    b.HasIndex("CalculationUnitId");

                    b.ToTable("calculation_result_item", (string)null);
                });

            modelBuilder.Entity("CalculationEngine.Core.Model.CalculationUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("GraphId")
                        .HasColumnType("uuid")
                        .HasColumnName("graph_id");

                    b.Property<string>("JobId")
                        .HasColumnType("text")
                        .HasColumnName("job_id");

                    b.Property<string>("_requestJson")
                        .HasColumnType("jsonb")
                        .HasColumnName("request_json");

                    b.Property<string>("_requestType")
                        .HasColumnType("text")
                        .HasColumnName("request_type");

                    b.HasKey("Id")
                        .HasName("calculation_unit_pkey");

                    b.ToTable("calculation_unit", (string)null);
                });

            modelBuilder.Entity("CalculationEngine.Core.Model.CalculationResultItem", b =>
                {
                    b.HasOne("CalculationEngine.Core.Model.CalculationUnit", "CalculationUnit")
                        .WithMany("Results")
                        .HasForeignKey("CalculationUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CalculationUnit");
                });

            modelBuilder.Entity("CalculationEngine.Core.Model.CalculationUnit", b =>
                {
                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
