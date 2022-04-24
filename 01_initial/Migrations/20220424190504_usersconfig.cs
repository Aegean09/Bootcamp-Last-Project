using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_initial.Migrations
{
    public partial class usersconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPromoter",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isTicket",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPromoter",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "isTicket",
                table: "Events");
        }
    }
}
