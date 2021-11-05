using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class refontedelatableaprespleindemauvaisemanip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "Event",
    columns: table => new
    {
        Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        Nature = table.Column<string>(nullable: true),
        Date = table.Column<DateTime>(nullable: false),
        Date_of_creation = table.Column<DateTime>(nullable: false),
        Type = table.Column<string>(nullable: true),
        Type_Id = table.Column<int>(nullable: false),
        Classe_Id = table.Column<int>(nullable: false),
        User = table.Column<string>(nullable: true),
        Heures = table.Column<DateTime>(nullable: false),
        commente = table.Column<string>(nullable: true)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Event", x => x.Id);
    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
