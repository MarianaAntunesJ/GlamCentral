using Microsoft.EntityFrameworkCore.Migrations;

namespace GlamCentral.Migrations
{
    public partial class ClienteDependente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Pagamentos",
                newName: "FormaDePagamento");

            migrationBuilder.RenameColumn(
                name: "Agenda",
                table: "Pagamentos",
                newName: "AgendamentoId");

            migrationBuilder.AddColumn<string>(
                name: "Desconto",
                table: "Pagamentos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Pagamentos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDependente",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ResponsavelId",
                table: "Clientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_AgendamentoId",
                table: "Pagamentos",
                column: "AgendamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ResponsavelId",
                table: "Clientes",
                column: "ResponsavelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Clientes_ResponsavelId",
                table: "Clientes",
                column: "ResponsavelId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Agenda_AgendamentoId",
                table: "Pagamentos",
                column: "AgendamentoId",
                principalTable: "Agenda",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Clientes_ResponsavelId",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Agenda_AgendamentoId",
                table: "Pagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Pagamentos_AgendamentoId",
                table: "Pagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_ResponsavelId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Desconto",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "IsDependente",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ResponsavelId",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "FormaDePagamento",
                table: "Pagamentos",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "AgendamentoId",
                table: "Pagamentos",
                newName: "Agenda");
        }
    }
}
