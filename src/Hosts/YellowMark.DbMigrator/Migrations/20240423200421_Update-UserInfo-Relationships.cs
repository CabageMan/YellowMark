using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowMark.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserInfoRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfos_AspNetUsers_AccountId",
                table: "UsersInfos");

            migrationBuilder.DropIndex(
                name: "IX_UsersInfos_AccountId",
                table: "UsersInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersInfos_AccountId",
                table: "UsersInfos",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfos_AspNetUsers_AccountId",
                table: "UsersInfos",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
