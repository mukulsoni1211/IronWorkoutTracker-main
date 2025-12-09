using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronWorkoutTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderIntoProgramDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ProgramDays",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "ProgramDays");
        }
    }
}
