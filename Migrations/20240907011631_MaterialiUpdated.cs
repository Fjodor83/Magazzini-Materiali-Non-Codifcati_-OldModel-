using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagazziniMaterialiAPI.Migrations
{
    /// <inheritdoc />
    public partial class MaterialiUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QRCodeData",
                table: "MaterialeImmagini",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeData",
                table: "MaterialeImmagini");
        }
    }
}
