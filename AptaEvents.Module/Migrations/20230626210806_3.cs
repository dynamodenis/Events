using System;
using System.Collections.Generic;
using AptaEvents.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptaEvents.Module.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Live = table.Column<bool>(type: "boolean", nullable: false),
                    EventLink = table.Column<string>(type: "text", nullable: true),
                    EventFields = table.Column<IList<EventField>>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
