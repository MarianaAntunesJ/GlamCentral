using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GlamCentral.Migrations
{
    public partial class AgendaDuracaoInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Agenda");

            migrationBuilder.AddColumn<int>(
                name: "Duracao",
                table: "Agenda",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Agenda");

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration",
                table: "Agenda",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
