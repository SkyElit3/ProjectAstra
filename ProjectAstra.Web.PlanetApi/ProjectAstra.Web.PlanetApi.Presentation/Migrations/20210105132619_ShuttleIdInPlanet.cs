using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAstra.Web.PlanetApi.Presentation.Migrations
{
    public partial class ShuttleIdInPlanet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShuttleId",
                table: "Planets",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShuttleId",
                table: "Planets");
        }
    }
}
