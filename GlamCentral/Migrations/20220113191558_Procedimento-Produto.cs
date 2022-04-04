using Microsoft.EntityFrameworkCore.Migrations;

namespace GlamCentral.Migrations
{
    public partial class ProcedimentoProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Procedimentos_ProdutosId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_ProdutosId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ProdutosId",
                table: "Produtos");

            migrationBuilder.CreateIndex(
                name: "IX_Procedimentos_ProdutosId",
                table: "Procedimentos",
                column: "ProdutosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedimentos_Produtos_ProdutosId",
                table: "Procedimentos",
                column: "ProdutosId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedimentos_Produtos_ProdutosId",
                table: "Procedimentos");

            migrationBuilder.DropIndex(
                name: "IX_Procedimentos_ProdutosId",
                table: "Procedimentos");

            migrationBuilder.AddColumn<int>(
                name: "ProdutosId",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ProdutosId",
                table: "Produtos",
                column: "ProdutosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Procedimentos_ProdutosId",
                table: "Produtos",
                column: "ProdutosId",
                principalTable: "Procedimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
