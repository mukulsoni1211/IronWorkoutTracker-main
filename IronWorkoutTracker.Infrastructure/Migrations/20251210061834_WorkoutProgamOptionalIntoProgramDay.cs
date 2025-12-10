using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronWorkoutTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WorkoutProgamOptionalIntoProgramDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramDays_Users_UserId",
                table: "ProgramDays");

            migrationBuilder.DropIndex(
                name: "IX_ProgramDays_UserId",
                table: "ProgramDays");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProgramDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProgramDays",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDays_UserId",
                table: "ProgramDays",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramDays_Users_UserId",
                table: "ProgramDays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
