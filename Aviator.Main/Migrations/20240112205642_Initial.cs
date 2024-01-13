using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aviator.Main.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Outputs",
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
                    table.PrimaryKey("PK_Outputs", x => x.Guid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outputs");
        }
    }
}
