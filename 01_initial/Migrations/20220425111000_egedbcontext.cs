using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_initial.Migrations
{
    public partial class egedbcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventsEventId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAttender",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EventsEventId",
                table: "Users",
                column: "EventsEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Events_EventsEventId",
                table: "Users",
                column: "EventsEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Events_EventsEventId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EventsEventId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EventsEventId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAttender",
                table: "Users");
        }
    }
}
