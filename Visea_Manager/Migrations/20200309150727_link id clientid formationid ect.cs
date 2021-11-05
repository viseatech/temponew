using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class linkidclientidformationidect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mission_Id",
                table: "Type_Mission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "formation_Id",
                table: "Type_formation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Client_Id",
                table: "Mission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type_Mission_Id",
                table: "Etape_Mission",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mission_Id",
                table: "Type_Mission");

            migrationBuilder.DropColumn(
                name: "formation_Id",
                table: "Type_formation");

            migrationBuilder.DropColumn(
                name: "Client_Id",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "type_Mission_Id",
                table: "Etape_Mission");
        }
    }
}
