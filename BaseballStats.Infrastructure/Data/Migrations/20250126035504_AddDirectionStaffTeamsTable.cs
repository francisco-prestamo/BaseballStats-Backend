using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectionStaffTeamsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectionStaffTeam_DirectionStaff_DirectionStaffsId",
                table: "DirectionStaffTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectionStaffTeam_Team_TeamsLeadId",
                table: "DirectionStaffTeam");

            migrationBuilder.RenameColumn(
                name: "TeamsLeadId",
                table: "DirectionStaffTeam",
                newName: "TeamId");

            migrationBuilder.RenameColumn(
                name: "DirectionStaffsId",
                table: "DirectionStaffTeam",
                newName: "DirectionStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectionStaffTeam_TeamsLeadId",
                table: "DirectionStaffTeam",
                newName: "IX_DirectionStaffTeam_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectionStaffTeam_DirectionStaff_DirectionStaffId",
                table: "DirectionStaffTeam",
                column: "DirectionStaffId",
                principalTable: "DirectionStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectionStaffTeam_Team_TeamId",
                table: "DirectionStaffTeam",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectionStaffTeam_DirectionStaff_DirectionStaffId",
                table: "DirectionStaffTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectionStaffTeam_Team_TeamId",
                table: "DirectionStaffTeam");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "DirectionStaffTeam",
                newName: "TeamsLeadId");

            migrationBuilder.RenameColumn(
                name: "DirectionStaffId",
                table: "DirectionStaffTeam",
                newName: "DirectionStaffsId");

            migrationBuilder.RenameIndex(
                name: "IX_DirectionStaffTeam_TeamId",
                table: "DirectionStaffTeam",
                newName: "IX_DirectionStaffTeam_TeamsLeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectionStaffTeam_DirectionStaff_DirectionStaffsId",
                table: "DirectionStaffTeam",
                column: "DirectionStaffsId",
                principalTable: "DirectionStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectionStaffTeam_Team_TeamsLeadId",
                table: "DirectionStaffTeam",
                column: "TeamsLeadId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
