using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aviator.Main.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Outputs",
                columns: new[] { "Guid", "Address", "Decoder", "Port", "Protocol" },
                values: new object[,]
                {
                    { new Guid("53703d00-aba1-4d0b-b7c5-50dbea253659"), "feed.airframes.io", 4, 5550, 1 },
                    { new Guid("74ec7f80-000c-4c23-b42f-5512dc9a21ff"), "feed.airframes.io", 0, 5552, 1 },
                    { new Guid("78f40023-5ddf-480e-aae9-a0c8e1a4ff2e"), "feed.airframes.io", 1, 5556, 0 },
                    { new Guid("a456e7d4-6101-4b6d-8193-126058188b07"), "feed.airframes.io", 2, 5571, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Outputs",
                keyColumn: "Guid",
                keyValue: new Guid("53703d00-aba1-4d0b-b7c5-50dbea253659"));

            migrationBuilder.DeleteData(
                table: "Outputs",
                keyColumn: "Guid",
                keyValue: new Guid("74ec7f80-000c-4c23-b42f-5512dc9a21ff"));

            migrationBuilder.DeleteData(
                table: "Outputs",
                keyColumn: "Guid",
                keyValue: new Guid("78f40023-5ddf-480e-aae9-a0c8e1a4ff2e"));

            migrationBuilder.DeleteData(
                table: "Outputs",
                keyColumn: "Guid",
                keyValue: new Guid("a456e7d4-6101-4b6d-8193-126058188b07"));
        }
    }
}
