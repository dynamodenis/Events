using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptaEvents.Module.Migrations
{
    /// <inheritdoc />
    public partial class EventTournamentIdStandard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "Events",
                newName: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Events",
                newName: "TournamentId");
        }
    }
}
