using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class addfilenoteid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Zfile",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Zfile");
        }
    }
}
