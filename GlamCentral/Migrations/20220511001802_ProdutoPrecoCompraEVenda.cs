using Microsoft.EntityFrameworkCore.Migrations;

namespace GlamCentral.Migrations
{
    public partial class ProdutoPrecoCompraEVenda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PrecoCompra",
                table: "Produtos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PrecoVenda",
                table: "Produtos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoCompra",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PrecoVenda",
                table: "Produtos");
        }
    }
}
