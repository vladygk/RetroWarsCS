using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroWars.Data.Migrations
{
    public partial class AddApplicationUsercollectiontopollstotrachwhohasvoted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserPoll",
                columns: table => new
                {
                    PollsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPoll", x => new { x.PollsId, x.VotersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserPoll_AspNetUsers_VotersId",
                        column: x => x.VotersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPoll_Polls_PollsId",
                        column: x => x.PollsId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPoll_VotersId",
                table: "ApplicationUserPoll",
                column: "VotersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserPoll");
        }
    }
}
