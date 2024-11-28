using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixPlayerInSeriesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerInSeries_TeamId",
                table: "PlayerInSeries");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInSeries_TeamId",
                table: "PlayerInSeries",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerInSeries_TeamId",
                table: "PlayerInSeries");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerInSeries_TeamId",
                table: "PlayerInSeries",
                column: "TeamId",
                unique: true);
        }
    }
}
