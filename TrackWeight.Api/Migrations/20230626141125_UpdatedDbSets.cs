using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackWeight.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalorieRecord_Users_UserId",
                table: "CalorieRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightRecord_Users_UserId",
                table: "WeightRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightRecord",
                table: "WeightRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalorieRecord",
                table: "CalorieRecord");

            migrationBuilder.RenameTable(
                name: "WeightRecord",
                newName: "WeightRecords");

            migrationBuilder.RenameTable(
                name: "CalorieRecord",
                newName: "CalorieRecords");

            migrationBuilder.RenameIndex(
                name: "IX_WeightRecord_UserId",
                table: "WeightRecords",
                newName: "IX_WeightRecords_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CalorieRecord_UserId",
                table: "CalorieRecords",
                newName: "IX_CalorieRecords_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightRecords",
                table: "WeightRecords",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalorieRecords",
                table: "CalorieRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CalorieRecords_Users_UserId",
                table: "CalorieRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightRecords_Users_UserId",
                table: "WeightRecords",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CalorieRecords_Users_UserId",
                table: "CalorieRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightRecords_Users_UserId",
                table: "WeightRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightRecords",
                table: "WeightRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CalorieRecords",
                table: "CalorieRecords");

            migrationBuilder.RenameTable(
                name: "WeightRecords",
                newName: "WeightRecord");

            migrationBuilder.RenameTable(
                name: "CalorieRecords",
                newName: "CalorieRecord");

            migrationBuilder.RenameIndex(
                name: "IX_WeightRecords_UserId",
                table: "WeightRecord",
                newName: "IX_WeightRecord_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CalorieRecords_UserId",
                table: "CalorieRecord",
                newName: "IX_CalorieRecord_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightRecord",
                table: "WeightRecord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CalorieRecord",
                table: "CalorieRecord",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CalorieRecord_Users_UserId",
                table: "CalorieRecord",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightRecord_Users_UserId",
                table: "WeightRecord",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
