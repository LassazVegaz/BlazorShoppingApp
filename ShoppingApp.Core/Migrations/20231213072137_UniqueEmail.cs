using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Core.Migrations
{
    /// <inheritdoc />
    public partial class UniqueEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_unique_email",
                table: "Users",
                sql: "UNIQUE(Email)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_unique_email",
                table: "Users");
        }
    }
}
