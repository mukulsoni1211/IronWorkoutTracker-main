using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronWorkoutTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToUserProgramEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserPrograms",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserPrograms");
        }
    }
}
