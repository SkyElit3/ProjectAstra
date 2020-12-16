using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    public partial class UniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamsOfExplorers_Name",
                table: "TeamsOfExplorers");

            migrationBuilder.DropIndex(
                name: "IX_Shuttles_Name",
                table: "Shuttles");

            migrationBuilder.DropIndex(
                name: "IX_Explorers_Name",
                table: "Explorers");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsOfExplorers_Name",
                table: "TeamsOfExplorers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shuttles_Name",
                table: "Shuttles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Explorers_Name",
                table: "Explorers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamsOfExplorers_Name",
                table: "TeamsOfExplorers");

            migrationBuilder.DropIndex(
                name: "IX_Shuttles_Name",
                table: "Shuttles");

            migrationBuilder.DropIndex(
                name: "IX_Explorers_Name",
                table: "Explorers");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsOfExplorers_Name",
                table: "TeamsOfExplorers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Shuttles_Name",
                table: "Shuttles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Explorers_Name",
                table: "Explorers",
                column: "Name");
        }
    }
}
