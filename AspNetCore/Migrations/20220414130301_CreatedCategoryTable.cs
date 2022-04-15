using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Migrations
{
    public partial class CreatedCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Abouts",
                type: "int",
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
    }
}
