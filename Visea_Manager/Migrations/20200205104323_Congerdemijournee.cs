using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class Congerdemijournee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Demijourne",
                table: "Conger",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "time",
                table: "Conger",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Demijourne",
                table: "Conger");

            migrationBuilder.DropColumn(
                name: "time",
                table: "Conger");
        }
    }
}
