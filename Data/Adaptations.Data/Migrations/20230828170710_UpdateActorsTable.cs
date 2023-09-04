using Microsoft.EntityFrameworkCore.Migrations;

namespace Adaptations.Data.Migrations
{
    public partial class UpdateActorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Images_ImageId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_ImageId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Actors");

            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ActorId",
                table: "Images",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Actors_ActorId",
                table: "Images",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Actors_ActorId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ActorId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Actors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ImageId",
                table: "Actors",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Images_ImageId",
                table: "Actors",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
