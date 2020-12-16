using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    public partial class AddTeamOfExplorersDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeamsOfExplorers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ShuttleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsOfExplorers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamsOfExplorers_Shuttles_ShuttleId",
                        column: x => x.ShuttleId,
                        principalTable: "Shuttles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamsOfExplorers_Name",
                table: "TeamsOfExplorers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsOfExplorers_ShuttleId",
                table: "TeamsOfExplorers",
                column: "ShuttleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamsOfExplorers");
        }
    }
}
