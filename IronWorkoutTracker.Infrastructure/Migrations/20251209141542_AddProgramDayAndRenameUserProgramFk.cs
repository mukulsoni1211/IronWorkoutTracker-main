using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IronWorkoutTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProgramDayAndRenameUserProgramFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPrograms_WorkoutPrograms_ProgramId",
                table: "UserPrograms");

            migrationBuilder.RenameColumn(
                name: "ProgramId",
                table: "UserPrograms",
                newName: "WorkoutProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPrograms_ProgramId",
                table: "UserPrograms",
                newName: "IX_UserPrograms_WorkoutProgramId");

            migrationBuilder.CreateTable(
                name: "ProgramDays",
                columns: table => new
                {
                    ProgramDayId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    WorkoutProgramId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDays", x => x.ProgramDayId);
                    table.ForeignKey(
                        name: "FK_ProgramDays_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramDays_WorkoutPrograms_WorkoutProgramId",
                        column: x => x.WorkoutProgramId,
                        principalTable: "WorkoutPrograms",
                        principalColumn: "WorkoutProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDays_UserId",
                table: "ProgramDays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDays_WorkoutProgramId",
                table: "ProgramDays",
                column: "WorkoutProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrograms_WorkoutPrograms_WorkoutProgramId",
                table: "UserPrograms",
                column: "WorkoutProgramId",
                principalTable: "WorkoutPrograms",
                principalColumn: "WorkoutProgramId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPrograms_WorkoutPrograms_WorkoutProgramId",
                table: "UserPrograms");

            migrationBuilder.DropTable(
                name: "ProgramDays");

            migrationBuilder.RenameColumn(
                name: "WorkoutProgramId",
                table: "UserPrograms",
                newName: "ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPrograms_WorkoutProgramId",
                table: "UserPrograms",
                newName: "IX_UserPrograms_ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrograms_WorkoutPrograms_ProgramId",
                table: "UserPrograms",
                column: "ProgramId",
                principalTable: "WorkoutPrograms",
                principalColumn: "WorkoutProgramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
