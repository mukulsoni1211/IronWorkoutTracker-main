using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronWorkoutTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutDayExerciseNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_ProgramDayId",
                table: "WorkoutDays",
                column: "ProgramDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDayExercises_ExerciseId",
                table: "WorkoutDayExercises",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDayExercises_Exercises_ExerciseId",
                table: "WorkoutDayExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutDays_ProgramDays_ProgramDayId",
                table: "WorkoutDays",
                column: "ProgramDayId",
                principalTable: "ProgramDays",
                principalColumn: "ProgramDayId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDayExercises_Exercises_ExerciseId",
                table: "WorkoutDayExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutDays_ProgramDays_ProgramDayId",
                table: "WorkoutDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutDays_ProgramDayId",
                table: "WorkoutDays");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutDayExercises_ExerciseId",
                table: "WorkoutDayExercises");
        }
    }
}
