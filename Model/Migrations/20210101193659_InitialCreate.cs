using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    PostId = table.Column<Guid>(nullable: false),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    postAsHtml = table.Column<string>(nullable: true),
                    postAsMarkdown = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    bio = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    createdOn = table.Column<DateTime>(nullable: false),
                    updatedOn = table.Column<DateTime>(nullable: false),
                    comment = table.Column<string>(nullable: true),
                    PostId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostInfo",
                columns: table => new
                {
                    PostInfoId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    recap = table.Column<string>(nullable: true),
                    titel = table.Column<string>(nullable: true),
                    postStatus = table.Column<int>(nullable: false),
                    publishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostInfo", x => x.PostInfoId);
                    table.ForeignKey(
                        name: "FK_PostInfo_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_PostId",
                table: "Comment",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostInfo_PostId",
                table: "PostInfo",
                column: "PostId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "PostInfo");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Post");
        }
    }
}
