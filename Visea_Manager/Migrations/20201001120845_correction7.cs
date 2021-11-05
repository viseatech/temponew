using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Visea_Expense_Manager.Migrations
{
    public partial class correction7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conger",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Date_Debut = table.Column<DateTime>(nullable: false),
                    Date_Fin = table.Column<DateTime>(nullable: false),
                    Demijourne = table.Column<int>(nullable: false),
                    time = table.Column<float>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Director = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    commente = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conger", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conger");
        }
    }
}
