using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Core.Migrations;

/// <inheritdoc />
public partial class EmailUpdatedOnCol : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateOnly>(
            name: "EmailUpdatedOn",
            table: "Users",
            type: "date",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "EmailUpdatedOn",
            table: "Users");
    }
}
