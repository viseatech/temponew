using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class Classestr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Classe2_str",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Classe3_str",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Classe4_str",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Classe_str",
                table: "Event",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classe2_str",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Classe3_str",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Classe4_str",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Classe_str",
                table: "Event");
        }
    }
}
