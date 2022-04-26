using Microsoft.EntityFrameworkCore.Migrations;

namespace GlamCentral.Migrations
{
    public partial class AgendaEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Procedimento",
                table: "Pagamentos",
                newName: "Agenda");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Agenda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuncionarioId",
                table: "Agenda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProcedimentoId",
                table: "Agenda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_ClienteId",
                table: "Agenda",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_FuncionarioId",
                table: "Agenda",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_ProcedimentoId",
                table: "Agenda",
                column: "ProcedimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Clientes_ClienteId",
                table: "Agenda",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Funcionarios_FuncionarioId",
                table: "Agenda",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_Procedimentos_ProcedimentoId",
                table: "Agenda",
                column: "ProcedimentoId",
                principalTable: "Procedimentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Clientes_ClienteId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Funcionarios_FuncionarioId",
                table: "Agenda");

            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_Procedimentos_ProcedimentoId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_ClienteId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_FuncionarioId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_ProcedimentoId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "FuncionarioId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "ProcedimentoId",
                table: "Agenda");

            migrationBuilder.RenameColumn(
                name: "Agenda",
                table: "Pagamentos",
                newName: "Procedimento");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
