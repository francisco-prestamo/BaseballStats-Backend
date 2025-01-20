using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAlignedPlayerInGame_PlayerInPositionForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AlignedPlayerInGame_PlayerId_Position",
                table: "AlignedPlayerInGame",
                columns: new[] { "PlayerId", "Position" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlignedPlayerInGame_PlayerInPosition_PlayerId_Position",
                table: "AlignedPlayerInGame",
                columns: new[] { "PlayerId", "Position" },
                principalTable: "PlayerInPosition",
                principalColumns: new[] { "PlayerId", "Position" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlignedPlayerInGame_PlayerInPosition_PlayerId_Position",
                table: "AlignedPlayerInGame");

            migrationBuilder.DropIndex(
                name: "IX_AlignedPlayerInGame_PlayerId_Position",
                table: "AlignedPlayerInGame");
        }
    }
}
