using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroWars.Data.Migrations
{
    public partial class AddmanytomanybetweenApplicationUsersandGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_ApplicationUserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_ApplicationUserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "ApplicationUserGame",
                columns: table => new
                {
                    FavoriteGamesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGame", x => new { x.FavoriteGamesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGame_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGame_Games_FavoriteGamesId",
                        column: x => x.FavoriteGamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGame_UsersId",
                table: "ApplicationUserGame",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGame");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_ApplicationUserId",
                table: "Games",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_ApplicationUserId",
                table: "Games",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
