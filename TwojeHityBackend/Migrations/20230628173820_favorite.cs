using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwojeHity.Migrations
{
    /// <inheritdoc />
    public partial class favorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_userId = table.Column<int>(type: "int", nullable: true),
                    ID_songId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Songs_ID_songId",
                        column: x => x.ID_songId,
                        principalTable: "Songs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorites_Users_ID_userId",
                        column: x => x.ID_userId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ID_songId",
                table: "Favorites",
                column: "ID_songId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ID_userId",
                table: "Favorites",
                column: "ID_userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");
        }
    }
}
