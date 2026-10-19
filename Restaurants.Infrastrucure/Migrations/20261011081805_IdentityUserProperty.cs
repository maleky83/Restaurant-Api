using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastrucure.Migrations;

/// <inheritdoc />
public partial class IdentityUserProperty : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateOnly>(
            name: "DateOfBirth",
            table: "AspNetUsers",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1));

        migrationBuilder.AddColumn<string>(
            name: "Nationality",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DateOfBirth",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "Nationality",
            table: "AspNetUsers");
    }
}
