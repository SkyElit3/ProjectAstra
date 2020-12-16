using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    public partial class AddExplorersDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Explorers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ExplorerType = table.Column<string>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UnitsOfEnergy = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Explorers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Explorers");
        }
    }
}
