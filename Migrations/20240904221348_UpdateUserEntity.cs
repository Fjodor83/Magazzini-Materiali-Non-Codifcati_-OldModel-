using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazziniMaterialiAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialeImmagine");

            migrationBuilder.AddColumn<string>(
                name: "Cognome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ruolo",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaterialeImmagini",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlImmagine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrincipale = table.Column<bool>(type: "bit", nullable: false),
                    MaterialeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialeImmagini", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialeImmagini_Materiali_MaterialeId",
                        column: x => x.MaterialeId,
                        principalTable: "Materiali",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeImmagini_MaterialeId",
                table: "MaterialeImmagini",
                column: "MaterialeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialeImmagini");

            migrationBuilder.DropColumn(
                name: "Cognome",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ruolo",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "MaterialeImmagine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPrincipale = table.Column<bool>(type: "bit", nullable: false),
                    MaterialeId = table.Column<int>(type: "int", nullable: true),
                    UrlImmagine = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialeImmagine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialeImmagine_Materiali_MaterialeId",
                        column: x => x.MaterialeId,
                        principalTable: "Materiali",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeImmagine_MaterialeId",
                table: "MaterialeImmagine",
                column: "MaterialeId");
        }
    }
}
