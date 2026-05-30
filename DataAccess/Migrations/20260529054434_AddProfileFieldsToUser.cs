using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileFieldsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
