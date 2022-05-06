using Microsoft.EntityFrameworkCore.Migrations;

namespace GlamCentral.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ProdutosDeProcedimento",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    ProcedimentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosDeProcedimento", x => new { x.ProdutoId, x.ProcedimentoId });
                    table.ForeignKey(
                        name: "FK_ProdutosDeProcedimento_Procedimentos_ProcedimentoId",
                        column: x => x.ProcedimentoId,
                        principalTable: "Procedimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutosDeProcedimento_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosDeProcedimento_ProcedimentoId",
                table: "ProdutosDeProcedimento",
                column: "ProcedimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutosDeProcedimento");

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
    }
}
