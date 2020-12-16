using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    public partial class AddExplorersLinkedToTOEDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamOfExplorersId",
                table: "Explorers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Explorers_TeamOfExplorersId",
                table: "Explorers",
                column: "TeamOfExplorersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Explorers_TeamsOfExplorers_TeamOfExplorersId",
                table: "Explorers",
                column: "TeamOfExplorersId",
                principalTable: "TeamsOfExplorers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explorers_TeamsOfExplorers_TeamOfExplorersId",
                table: "Explorers");

            migrationBuilder.DropIndex(
                name: "IX_Explorers_TeamOfExplorersId",
                table: "Explorers");

            migrationBuilder.DropColumn(
                name: "TeamOfExplorersId",
                table: "Explorers");
        }
    }
}
