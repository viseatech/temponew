using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class addnotestate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Note",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Note");
        }
    }
}
