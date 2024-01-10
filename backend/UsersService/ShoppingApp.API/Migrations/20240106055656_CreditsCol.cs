using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Core.Migrations
{
    /// <inheritdoc />
    public partial class CreditsCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Credits",
                table: "Users",
                type: "double",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credits",
                table: "Users");
        }
    }
}
