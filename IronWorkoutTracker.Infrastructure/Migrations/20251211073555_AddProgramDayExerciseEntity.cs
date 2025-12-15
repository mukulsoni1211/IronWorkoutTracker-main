using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IronWorkoutTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramDayExerciseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgramDayExercises",
                columns: table => new
                {
                    ProgramDayExerciseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramDayId = table.Column<int>(type: "integer", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false),
                    TrainingStyle = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDayExercises", x => x.ProgramDayExerciseId);
                    table.ForeignKey(
                        name: "FK_ProgramDayExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramDayExercises_ProgramDays_ProgramDayId",
                        column: x => x.ProgramDayId,
                        principalTable: "ProgramDays",
                        principalColumn: "ProgramDayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDayExercises_ExerciseId",
                table: "ProgramDayExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDayExercises_ProgramDayId",
                table: "ProgramDayExercises",
                column: "ProgramDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramDayExercises");
        }
    }
}
