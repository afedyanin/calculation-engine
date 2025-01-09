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

                    b.Property<string>("Metadata")
                        .HasColumnType("text")
                        .HasColumnName("metadata");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PayloadJson")
                        .HasColumnType("jsonb")
                        .HasColumnName("payload_json");

                    b.HasKey("Id")
                        .HasName("calculation_result_item_pkey");

                    b.ToTable("calculation_result_item", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
