using Microsoft.EntityFrameworkCore.Migrations;

namespace Adaptations.Data.Migrations
{
    public partial class MakeChangesToAdaptations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CharacterDescription",
                table: "Characters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Authors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_BookId",
                table: "Images",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AddedByUserId",
                table: "Books",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_AddedByUserId",
                table: "Books",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_AddedByUserId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Books_BookId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_BookId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Books_AddedByUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CharacterDescription",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Authors");

            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Authors",
                nullable: false,
                defaultValue: 0);
        }
    }
}
