using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IronWorkoutTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserprogramWorkoutProgramEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkoutPrograms",
                columns: table => new
                {
                    WorkoutProgramId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    Visibility = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPrograms", x => x.WorkoutProgramId);
                    table.ForeignKey(
                        name: "FK_WorkoutPrograms_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPrograms",
                columns: table => new
                {
                    UserProgramId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProgramId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPrograms", x => x.UserProgramId);
                    table.ForeignKey(
                        name: "FK_UserPrograms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPrograms_WorkoutPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "WorkoutPrograms",
                        principalColumn: "WorkoutProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPrograms_ProgramId",
                table: "UserPrograms",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPrograms_UserId",
                table: "UserPrograms",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPrograms_CreatedById",
                table: "WorkoutPrograms",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPrograms");

            migrationBuilder.DropTable(
                name: "WorkoutPrograms");
        }
    }
}
