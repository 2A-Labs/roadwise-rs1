using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roadwise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructorAndCandidateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmployee",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IsManager",
                table: "Users",
                newName: "IsInstructor");

            migrationBuilder.AddColumn<bool>(
                name: "IsCandidate",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCandidate",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "IsInstructor",
                table: "Users",
                newName: "IsManager");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployee",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
