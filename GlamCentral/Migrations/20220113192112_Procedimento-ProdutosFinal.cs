using Microsoft.EntityFrameworkCore.Migrations;

namespace GlamCentral.Migrations
{
    public partial class ProcedimentoProdutosFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProdutosId",
                table: "Procedimentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropColumn(
                name: "ProdutosId",
                table: "Procedimentos");
        }
    }
}
