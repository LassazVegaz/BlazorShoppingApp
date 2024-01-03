using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.Core.Migrations;

/// <inheritdoc />
public partial class DOBAndGender : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateOnly>(
            name: "DateOfBirth",
            table: "Users",
            type: "date",
            nullable: false,
            defaultValue: null);

        migrationBuilder.AddColumn<string>(
            name: "Gender",
            table: "Users",
            type: "longtext",
            nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AddCheckConstraint(
            name: "CK_gender",
            table: "Users",
            sql: "gender in ('male', 'female', 'other')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropCheckConstraint(
            name: "CK_gender",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "DateOfBirth",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "Gender",
            table: "Users");
    }
}
