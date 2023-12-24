using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Core.Migrations
{
    /// <inheritdoc />
    public partial class GenderConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_users_gender",
                table: "AspNetUsers",
                sql: "gender in ('male', 'female', 'other')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_users_gender",
                table: "AspNetUsers");
        }
    }
}
