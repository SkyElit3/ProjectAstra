using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAstra.Web.CrewApi.Presentation.Migrations
{
    public partial class DEBUGIndexesv1 : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TeamsOfExplorers",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TeamsOfExplorers",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shuttles",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Explorers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Explorers",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Explorers",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Explorers",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET latin1",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ExplorerType",
                table: "Explorers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET latin1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TeamsOfExplorers",
                type: "longtext CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TeamsOfExplorers",
                type: "varchar(255) CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Shuttles",
                type: "varchar(255) CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Explorers",
                type: "longtext CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Explorers",
                type: "longtext CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Explorers",
                type: "longtext CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Explorers",
                type: "varchar(255) CHARACTER SET latin1",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExplorerType",
                table: "Explorers",
                type: "longtext CHARACTER SET latin1",
                nullable: false,
                oldClrType: typeof(int));

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
