using Microsoft.EntityFrameworkCore.Migrations;

namespace GlamCentral.Migrations
{
    public partial class AgendaDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Agenda");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Agenda",
                newName: "Duration");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Agenda",
                newName: "End");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Agenda",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
