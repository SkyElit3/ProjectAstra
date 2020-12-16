using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    public partial class AddExplorersParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explorers_TeamsOfExplorers_TeamOfExplorersId",
                table: "Explorers");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamOfExplorersId",
                table: "Explorers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Explorers_TeamsOfExplorers_TeamOfExplorersId",
                table: "Explorers",
                column: "TeamOfExplorersId",
                principalTable: "TeamsOfExplorers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Explorers_TeamsOfExplorers_TeamOfExplorersId",
                table: "Explorers");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamOfExplorersId",
                table: "Explorers",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Explorers_TeamsOfExplorers_TeamOfExplorersId",
                table: "Explorers",
                column: "TeamOfExplorersId",
                principalTable: "TeamsOfExplorers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
