using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Migrations
{
    public partial class CreatedAboutAddTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Abouts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Abouts_TeacherId",
                table: "Abouts",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abouts_Teachers_TeacherId",
                table: "Abouts",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abouts_Teachers_TeacherId",
                table: "Abouts");

            migrationBuilder.DropIndex(
                name: "IX_Abouts_TeacherId",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Abouts");
        }
    }
}
