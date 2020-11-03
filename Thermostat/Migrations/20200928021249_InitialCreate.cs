using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Thermostat.Migrations
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HvacSensorHistory",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(nullable: false),
                    Data_IndoorTemp = table.Column<double>(nullable: true),
                    Data_OutdoorTemp = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HvacSensorHistory", x => x.DateTime);
                });

            migrationBuilder.CreateTable(
                name: "HvacSetPointHistory",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(nullable: false),
                    Data_MaxTemp = table.Column<double>(nullable: true),
                    Data_MinTemp = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HvacSetPointHistory", x => x.DateTime);
                });

            migrationBuilder.CreateTable(
                name: "HvacSystemStateHistory",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(nullable: false),
                    Data_IsCooling = table.Column<bool>(nullable: true),
                    Data_IsFanRunning = table.Column<bool>(nullable: true),
                    Data_IsHeating = table.Column<bool>(nullable: true),
                    Data_IsAuxHeat = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HvacSystemStateHistory", x => x.DateTime);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HvacSensorHistory");

            migrationBuilder.DropTable(
                name: "HvacSetPointHistory");

            migrationBuilder.DropTable(
                name: "HvacSystemStateHistory");
        }
    }
}
