using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_initial.Migrations
{
    public partial class usersconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Events_EventsEventId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EventsEventId",
                table: "Users",
                newName: "Events_I_AttendEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_EventsEventId",
                table: "Users",
                newName: "IX_Users_Events_I_AttendEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Events_Events_I_AttendEventId",
                table: "Users",
                column: "Events_I_AttendEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Events_Events_I_AttendEventId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Events_I_AttendEventId",
                table: "Users",
                newName: "EventsEventId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Events_I_AttendEventId",
                table: "Users",
                newName: "IX_Users_EventsEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Events_EventsEventId",
                table: "Users",
                column: "EventsEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
