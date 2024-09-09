using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazziniMaterialiAPI.Migrations
{
    /// <inheritdoc />
    public partial class Movimentazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialeImmagini_Materiali_MaterialeId",
                table: "MaterialeImmagini");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialeImmagini_Materiali_MaterialeId",
                table: "MaterialeImmagini",
                column: "MaterialeId",
                principalTable: "Materiali",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialeImmagini_Materiali_MaterialeId",
                table: "MaterialeImmagini");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialeImmagini_Materiali_MaterialeId",
                table: "MaterialeImmagini",
                column: "MaterialeId",
                principalTable: "Materiali",
                principalColumn: "Id");
        }
    }
}
