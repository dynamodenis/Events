using System;
using System.Collections.Generic;
using AptaEvents.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptaEvents.Module.Migrations
{
    /// <inheritdoc />
    public partial class nojson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventFields",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "EventFields",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Field = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    EventID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFields", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventFields_Events_EventID",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventFields_EventID",
                table: "EventFields",
                column: "EventID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventFields");

            migrationBuilder.AddColumn<IList<EventField>>(
                name: "EventFields",
                table: "Events",
                type: "jsonb",
                nullable: true);
        }
    }
}
