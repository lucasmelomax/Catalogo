using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_Oficial2.Migrations
{
    /// <inheritdoc />
    public partial class CampoInativo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Inativo",
                table: "Usuarios",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Inativo",
                table: "Produtos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Inativo",
                table: "Pedidos",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inativo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Inativo",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Inativo",
                table: "Pedidos");
        }
    }
}
