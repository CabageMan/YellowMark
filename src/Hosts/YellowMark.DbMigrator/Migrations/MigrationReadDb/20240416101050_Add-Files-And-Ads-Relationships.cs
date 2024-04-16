using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowMark.DbMigrator.Migrations.MigrationReadDb
{
    /// <inheritdoc />
    public partial class AddFilesAndAdsRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AdId",
                table: "Files",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Files_AdId",
                table: "Files",
                column: "AdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Ads_AdId",
                table: "Files",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Ads_AdId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_AdId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "AdId",
                table: "Files");
        }
    }
}
