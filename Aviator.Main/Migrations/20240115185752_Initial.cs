using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aviator.Main.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endpoints",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "TEXT", nullable: false),
                    Decoder = table.Column<int>(type: "INTEGER", nullable: false),
                    Protocol = table.Column<int>(type: "INTEGER", nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endpoints", x => x.Guid);
                });

            migrationBuilder.InsertData(
                table: "Endpoints",
                columns: new[] { "Guid", "Address", "Decoder", "Port", "Protocol" },
                values: new object[,]
                {
                    { new Guid("3ab42391-2fd9-4930-9351-a5c2481a52c5"), "feed.airframes.io", 4, 5550, 1 },
                    { new Guid("4c6518dd-ac1d-4347-9beb-6e3310c11701"), "feed.airframes.io", 0, 5552, 1 },
                    { new Guid("ce773214-564d-48b3-b345-b726cbe7d10d"), "feed.airframes.io", 2, 5571, 1 },
                    { new Guid("df456bcb-c580-4409-884f-20d517ad6925"), "feed.airframes.io", 1, 5556, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endpoints");
        }
    }
}
