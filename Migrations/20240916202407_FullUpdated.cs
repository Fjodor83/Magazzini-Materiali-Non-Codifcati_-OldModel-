using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazziniMaterialiAPI.Migrations
{
    /// <inheritdoc />
    public partial class FullUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialeMagazzini",
                columns: table => new
                {
                    MaterialeMagazzinoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialeID = table.Column<int>(type: "int", nullable: false),
                    MagazzinoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialeMagazzini", x => x.MaterialeMagazzinoID);
                    table.ForeignKey(
                        name: "FK_MaterialeMagazzini_Magazzini_MagazzinoID",
                        column: x => x.MagazzinoID,
                        principalTable: "Magazzini",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialeMagazzini_Materiali_MaterialeID",
                        column: x => x.MaterialeID,
                        principalTable: "Materiali",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeMagazzini_MagazzinoID",
                table: "MaterialeMagazzini",
                column: "MagazzinoID");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeMagazzini_MaterialeID",
                table: "MaterialeMagazzini",
                column: "MaterialeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialeMagazzini");
        }
    }
}
