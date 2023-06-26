using Microsoft.EntityFrameworkCore.Migrations;

namespace Adaptations.Data.Migrations
{
    public partial class MakeChangesToAdaptations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Actors_ActorId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ActorId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_BookId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoviePlot",
                table: "Movies",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemoteImageUrl",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_AddedByUserId",
                table: "Movies",
                column: "AddedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AddedByUserId",
                table: "Images",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_AddedByUserId",
                table: "Images",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_AddedByUserId",
                table: "Movies",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_AddedByUserId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_AddedByUserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_AddedByUserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Images_AddedByUserId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MoviePlot",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "RemoteImageUrl",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActorId",
                table: "Images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ActorId",
                table: "Images",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_BookId",
                table: "Images",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Actors_ActorId",
                table: "Images",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
