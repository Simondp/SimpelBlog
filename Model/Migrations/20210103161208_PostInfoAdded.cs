using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class PostInfoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostInfo_Posts_PostId",
                table: "PostInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostInfo",
                table: "PostInfo");

            migrationBuilder.RenameTable(
                name: "PostInfo",
                newName: "PostInfos");

            migrationBuilder.RenameIndex(
                name: "IX_PostInfo_PostId",
                table: "PostInfos",
                newName: "IX_PostInfos_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostInfos",
                table: "PostInfos",
                column: "PostInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostInfos_Posts_PostId",
                table: "PostInfos",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostInfos_Posts_PostId",
                table: "PostInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostInfos",
                table: "PostInfos");

            migrationBuilder.RenameTable(
                name: "PostInfos",
                newName: "PostInfo");

            migrationBuilder.RenameIndex(
                name: "IX_PostInfos_PostId",
                table: "PostInfo",
                newName: "IX_PostInfo_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostInfo",
                table: "PostInfo",
                column: "PostInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostInfo_Posts_PostId",
                table: "PostInfo",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
