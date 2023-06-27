using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AptaEvents.Module.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventFields");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventFields",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    FieldID = table.Column<Guid>(type: "uuid", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFields", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EventFields_Fields_FieldID",
                        column: x => x.FieldID,
                        principalTable: "Fields",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventFields_FieldID",
                table: "EventFields",
                column: "FieldID");
        }
    }
}
