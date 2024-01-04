using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemsService.Migrations;

/// <inheritdoc />
public partial class UserIdCol : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "UserId",
            table: "Items",
            type: "int",
            nullable: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "UserId",
            table: "Items");
    }
}
