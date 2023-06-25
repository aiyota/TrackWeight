using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWeight.Api.Migrations
{
    /// <inheritdoc />
    public partial class WeightRecordAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WeightRecord_UserId",
                table: "WeightRecord",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeightRecord_Users_UserId",
                table: "WeightRecord",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeightRecord_Users_UserId",
                table: "WeightRecord");

            migrationBuilder.DropIndex(
                name: "IX_WeightRecord_UserId",
                table: "WeightRecord");
        }
    }
}
