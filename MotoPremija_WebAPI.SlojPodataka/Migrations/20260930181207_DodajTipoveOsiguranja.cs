using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotoPremija_WebAPI.SlojPodataka.Migrations
{
    /// <inheritdoc />
    public partial class DodajTipoveOsiguranja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tiposiguranja",
                columns: new[] { "id", "baznapremija", "nazivosiguranja" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 100.0, "Osiguranje od odgovornosti" },
                    { new Guid("22222211-2222-2222-2222-222222222222"), 250.0, "Kasko osiguranje" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 150.0, "Osiguranje štete usled sudara" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 200.0, "Pokriće neosiguranog vozila" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_motocikl_korisnikid",
                table: "motocikl",
                column: "korisnikid");

            migrationBuilder.CreateIndex(
                name: "IX_osiguranje_motociklid",
                table: "osiguranje",
                column: "motociklid");

            migrationBuilder.CreateIndex(
                name: "IX_osiguranje_tiposiguranjaid",
                table: "osiguranje",
                column: "tiposiguranjaid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tiposiguranja",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "tiposiguranja",
                keyColumn: "id",
                keyValue: new Guid("22222211-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "tiposiguranja",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "tiposiguranja",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));
        }
    }
}
