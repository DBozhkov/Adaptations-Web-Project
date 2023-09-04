using Microsoft.EntityFrameworkCore.Migrations;

namespace Adaptations.Data.Migrations
{
    public partial class MakeChangesAdaptationsDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAlive",
                table: "Actors");

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Actors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Actors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Actors");

            migrationBuilder.AddColumn<bool>(
                name: "IsAlive",
                table: "Actors",
                nullable: false,
                defaultValue: false);
        }
    }
}
