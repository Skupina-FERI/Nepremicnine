using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RZ_nepremicnine.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PosredovanjeFK",
                table: "Nepremicnine",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PosredovanjeNavigationId",
                table: "Nepremicnine",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegijaFK",
                table: "Nepremicnine",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegijaNavigationId",
                table: "Nepremicnine",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VrstaNepremicnineFK",
                table: "Nepremicnine",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VrstaNepremicnineNavigationId",
                table: "Nepremicnine",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Posredovanja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posredovanja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrsteNepremicnin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteNepremicnin", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nepremicnine_PosredovanjeNavigationId",
                table: "Nepremicnine",
                column: "PosredovanjeNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Nepremicnine_RegijaNavigationId",
                table: "Nepremicnine",
                column: "RegijaNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Nepremicnine_VrstaNepremicnineNavigationId",
                table: "Nepremicnine",
                column: "VrstaNepremicnineNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nepremicnine_Posredovanja_PosredovanjeNavigationId",
                table: "Nepremicnine",
                column: "PosredovanjeNavigationId",
                principalTable: "Posredovanja",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nepremicnine_Regije_RegijaNavigationId",
                table: "Nepremicnine",
                column: "RegijaNavigationId",
                principalTable: "Regije",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nepremicnine_VrsteNepremicnin_VrstaNepremicnineNavigationId",
                table: "Nepremicnine",
                column: "VrstaNepremicnineNavigationId",
                principalTable: "VrsteNepremicnin",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nepremicnine_Posredovanja_PosredovanjeNavigationId",
                table: "Nepremicnine");

            migrationBuilder.DropForeignKey(
                name: "FK_Nepremicnine_Regije_RegijaNavigationId",
                table: "Nepremicnine");

            migrationBuilder.DropForeignKey(
                name: "FK_Nepremicnine_VrsteNepremicnin_VrstaNepremicnineNavigationId",
                table: "Nepremicnine");

            migrationBuilder.DropTable(
                name: "Posredovanja");

            migrationBuilder.DropTable(
                name: "Regije");

            migrationBuilder.DropTable(
                name: "VrsteNepremicnin");

            migrationBuilder.DropIndex(
                name: "IX_Nepremicnine_PosredovanjeNavigationId",
                table: "Nepremicnine");

            migrationBuilder.DropIndex(
                name: "IX_Nepremicnine_RegijaNavigationId",
                table: "Nepremicnine");

            migrationBuilder.DropIndex(
                name: "IX_Nepremicnine_VrstaNepremicnineNavigationId",
                table: "Nepremicnine");

            migrationBuilder.DropColumn(
                name: "PosredovanjeFK",
                table: "Nepremicnine");

            migrationBuilder.DropColumn(
                name: "PosredovanjeNavigationId",
                table: "Nepremicnine");

            migrationBuilder.DropColumn(
                name: "RegijaFK",
                table: "Nepremicnine");

            migrationBuilder.DropColumn(
                name: "RegijaNavigationId",
                table: "Nepremicnine");

            migrationBuilder.DropColumn(
                name: "VrstaNepremicnineFK",
                table: "Nepremicnine");

            migrationBuilder.DropColumn(
                name: "VrstaNepremicnineNavigationId",
                table: "Nepremicnine");
        }
    }
}
