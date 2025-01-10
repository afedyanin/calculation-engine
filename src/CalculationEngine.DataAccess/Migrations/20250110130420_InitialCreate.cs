using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalculationEngine.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calculation_graph",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    vertices_json = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("calculation_graph_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "calculation_unit",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    graph_id = table.Column<Guid>(type: "uuid", nullable: false),
                    job_id = table.Column<string>(type: "text", nullable: true),
                    request_json = table.Column<string>(type: "jsonb", nullable: true),
                    request_type = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("calculation_unit_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "calculation_result_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    calculation_unit_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    content_type = table.Column<string>(type: "text", nullable: true),
                    content_json = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("calculation_result_item_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_calculation_result_item_calculation_unit_calculation_unit_id",
                        column: x => x.calculation_unit_id,
                        principalTable: "calculation_unit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_calculation_result_item_calculation_unit_id",
                table: "calculation_result_item",
                column: "calculation_unit_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calculation_graph");

            migrationBuilder.DropTable(
                name: "calculation_result_item");

            migrationBuilder.DropTable(
                name: "calculation_unit");
        }
    }
}
