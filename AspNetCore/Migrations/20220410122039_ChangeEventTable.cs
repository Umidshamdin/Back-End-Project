using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Migrations
{
    public partial class ChangeEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Events_EventsId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventsId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventsId",
                table: "Events");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventDetailId",
                table: "Events",
                column: "EventDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventDetails_EventDetailId",
                table: "Events",
                column: "EventDetailId",
                principalTable: "EventDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventDetails_EventDetailId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventDetailId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventsId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventsId",
                table: "Events",
                column: "EventsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Events_EventsId",
                table: "Events",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
