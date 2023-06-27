using System;
using System.Collections.Generic;
using AptaEvents.Module.BusinessObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptaEvents.Module.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFields_Events_EventID",
                table: "EventFields");

            migrationBuilder.DropIndex(
                name: "IX_EventFields_EventID",
                table: "EventFields");

            migrationBuilder.DropColumn(
                name: "EventID",
                table: "EventFields");

            migrationBuilder.AddColumn<IList<EventField>>(
                name: "EventFields",
                table: "Events",
                type: "jsonb",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventFields",
                table: "Events");

            migrationBuilder.AddColumn<Guid>(
                name: "EventID",
                table: "EventFields",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventFields_EventID",
                table: "EventFields",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFields_Events_EventID",
                table: "EventFields",
                column: "EventID",
                principalTable: "Events",
                principalColumn: "ID");
        }
    }
}
