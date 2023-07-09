using Microsoft.EntityFrameworkCore.Migrations;

namespace Adaptations.Data.Migrations
{
    public partial class MakeAdditionalChangesAdaptations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Actors_ActorId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_ActorId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Actors_CharacterId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Characters");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Actors_CharacterId",
                table: "Actors",
                column: "CharacterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Actors_CharacterId",
                table: "Actors");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_ActorId",
                table: "Characters",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_CharacterId",
                table: "Actors",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Actors_ActorId",
                table: "Characters",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
