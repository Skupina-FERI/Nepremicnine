using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RZ_nepremicnine.Migrations
{
    /// <inheritdoc />
    public partial class AddRegijaAndTipNepremicnine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nepremicnine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Cena = table.Column<decimal>(type: "TEXT", nullable: false),
                    Regija = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Mesto = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Naslov = table.Column<string>(type: "TEXT", nullable: true),
                    TipNepremicnine = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Spalnic = table.Column<int>(type: "INTEGER", nullable: true),
                    Kopalnic = table.Column<int>(type: "INTEGER", nullable: true),
                    Kvadratura = table.Column<decimal>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UporabnikiId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nepremicnine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nepremicnine_AspNetUsers_UporabnikiId",
                        column: x => x.UporabnikiId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PropertyImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    NepremicninaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyImages_Nepremicnine_NepremicninaId",
                        column: x => x.NepremicninaId,
                        principalTable: "Nepremicnine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nepremicnine_UporabnikiId",
                table: "Nepremicnine",
                column: "UporabnikiId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyImages_NepremicninaId",
                table: "PropertyImages",
                column: "NepremicninaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyImages");

            migrationBuilder.DropTable(
                name: "Nepremicnine");
        }
    }
}
