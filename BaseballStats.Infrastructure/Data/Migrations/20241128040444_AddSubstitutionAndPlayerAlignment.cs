using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSubstitutionAndPlayerAlignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Game",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AlignedPlayerInGame",
                columns: table => new
                {
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlignedPlayerInGame", x => new { x.PlayerId, x.GameId, x.Position });
                    table.ForeignKey(
                        name: "FK_AlignedPlayerInGame_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlignedPlayerInGame_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlignedPlayerInGame_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Substitution",
                columns: table => new
                {
                    PlayerInId = table.Column<long>(type: "bigint", nullable: false),
                    PlayerOutId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    Time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    TeamId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substitution", x => new { x.PlayerInId, x.PlayerOutId, x.GameId, x.Time });
                    table.ForeignKey(
                        name: "FK_Substitution_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substitution_Player_PlayerInId",
                        column: x => x.PlayerInId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substitution_Player_PlayerOutId",
                        column: x => x.PlayerOutId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substitution_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_Team1Id_Team2Id_Date",
                table: "Game",
                columns: new[] { "Team1Id", "Team2Id", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlignedPlayerInGame_GameId",
                table: "AlignedPlayerInGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_AlignedPlayerInGame_PlayerId_GameId",
                table: "AlignedPlayerInGame",
                columns: new[] { "PlayerId", "GameId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlignedPlayerInGame_Position_TeamId_GameId",
                table: "AlignedPlayerInGame",
                columns: new[] { "Position", "TeamId", "GameId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlignedPlayerInGame_TeamId",
                table: "AlignedPlayerInGame",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitution_GameId",
                table: "Substitution",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitution_PlayerOutId",
                table: "Substitution",
                column: "PlayerOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Substitution_TeamId",
                table: "Substitution",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlignedPlayerInGame");

            migrationBuilder.DropTable(
                name: "Substitution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_Team1Id_Team2Id_Date",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Game");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                columns: new[] { "Team1Id", "Team2Id", "Date" });
        }
    }
}
