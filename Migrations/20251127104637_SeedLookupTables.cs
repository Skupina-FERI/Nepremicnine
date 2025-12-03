using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RZ_nepremicnine.Migrations
{
    /// <inheritdoc />
    public partial class SeedLookupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posredovanja",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Prodaja" },
                    { 2, "Oddaja" },
                    { 3, "Nakup" },
                    { 4, "Najem" }
                });

            migrationBuilder.InsertData(
                table: "Regije",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pomurska" },
                    { 2, "Podravska" },
                    { 3, "Koroška" },
                    { 4, "Savinjska" }
                });

            migrationBuilder.InsertData(
                table: "VrsteNepremicnin",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Stanovanje" },
                    { 2, "Hiša" },
                    { 3, "Parcela" },
                    { 4, "Poslovni prostor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posredovanja",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posredovanja",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posredovanja",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Posredovanja",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Regije",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Regije",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Regije",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Regije",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VrsteNepremicnin",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VrsteNepremicnin",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VrsteNepremicnin",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "VrsteNepremicnin",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
