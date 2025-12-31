using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronWorkoutTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutDayToUsrProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserProgramId",
                table: "WorkoutDays",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "WorkoutDayExerciseSets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RestSeconds",
                table: "WorkoutDayExerciseSets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Reps",
                table: "WorkoutDayExerciseSets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "RPE",
                table: "WorkoutDayExerciseSets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "WorkoutDayExerciseSets",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_UserProgramId",
                table: "WorkoutDays",
                column: "UserProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDays_UserPrograms_UserProgramId",
                table: "WorkoutDays",
                column: "UserProgramId",
                principalTable: "UserPrograms",
                principalColumn: "UserProgramId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDays_UserPrograms_UserProgramId",
                table: "WorkoutDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutDays_UserProgramId",
                table: "WorkoutDays");

            migrationBuilder.DropColumn(
                name: "UserProgramId",
                table: "WorkoutDays");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "WorkoutDayExerciseSets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RestSeconds",
                table: "WorkoutDayExerciseSets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Reps",
                table: "WorkoutDayExerciseSets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RPE",
                table: "WorkoutDayExerciseSets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "WorkoutDayExerciseSets",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
