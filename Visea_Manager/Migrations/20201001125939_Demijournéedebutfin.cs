using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class Demijournéedebutfin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Demijourne",
                table: "Conger");

            migrationBuilder.AddColumn<int>(
                name: "Demijourne_Debut",
                table: "Conger",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Demijourne_Fin",
                table: "Conger",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Demijourne_Debut",
                table: "Conger");

            migrationBuilder.DropColumn(
                name: "Demijourne_Fin",
                table: "Conger");

            migrationBuilder.AddColumn<int>(
                name: "Demijourne",
                table: "Conger",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
