using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwojeHity.Migrations
{
    /// <inheritdoc />
    public partial class fav2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Songs_ID_songId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_ID_userId",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "ID_userId",
                table: "Favorites",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID_songId",
                table: "Favorites",
                newName: "SongId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_ID_userId",
                table: "Favorites",
                newName: "IX_Favorites_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_ID_songId",
                table: "Favorites",
                newName: "IX_Favorites_SongId");

            migrationBuilder.AddColumn<int>(
                name: "ID_song",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_user",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Songs_SongId",
                table: "Favorites",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Songs_SongId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_UserId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "ID_song",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "ID_user",
                table: "Favorites");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Favorites",
                newName: "ID_userId");

            migrationBuilder.RenameColumn(
                name: "SongId",
                table: "Favorites",
                newName: "ID_songId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                newName: "IX_Favorites_ID_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites_SongId",
                table: "Favorites",
                newName: "IX_Favorites_ID_songId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Songs_ID_songId",
                table: "Favorites",
                column: "ID_songId",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_ID_userId",
                table: "Favorites",
                column: "ID_userId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
