using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StrategyGame.Infrastructure.Migrations
{
    public partial class Score : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scoreboard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrategyGameUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scoreboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scoreboard_AspNetUsers_StrategyGameUserId",
                        column: x => x.StrategyGameUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scoreboard_StrategyGameUserId",
                table: "Scoreboard",
                column: "StrategyGameUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scoreboard");
        }
    }
}
