using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    public partial class IndexOnExplorerName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Explorers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Explorers_Name",
                table: "Explorers",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Explorers_Name",
                table: "Explorers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Explorers",
                type: "longtext CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
