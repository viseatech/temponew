using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class fichenotedefrais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Client",
                table: "Note",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Note",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Note",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Debut",
                table: "Note",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_Fin",
                table: "Note",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Devises",
                table: "Note",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mission",
                table: "Note",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MissionId",
                table: "Note",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Montant_Rembourser",
                table: "Note",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Totale_Devises",
                table: "Note",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Totale_Euros",
                table: "Note",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Note",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "sous_autres",
                table: "Note",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "sous_deplacement",
                table: "Note",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "sous_frais_kilometrique",
                table: "Note",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "sous_hotel_resto",
                table: "Note",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "sous_voiture",
                table: "Note",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Client",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Date_Debut",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Date_Fin",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Devises",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Mission",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "MissionId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Montant_Rembourser",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Totale_Devises",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Totale_Euros",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "sous_autres",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "sous_deplacement",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "sous_frais_kilometrique",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "sous_hotel_resto",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "sous_voiture",
                table: "Note");
        }
    }
}
