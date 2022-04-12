using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Migrations
{
    public partial class ChangeFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseFeatures_FeatureId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "FeatureId",
                table: "Courses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseFeatureId",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseFeatures_FeatureId",
                table: "Courses",
                column: "FeatureId",
                principalTable: "CourseFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseFeatures_FeatureId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseFeatureId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "FeatureId",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseFeatures_FeatureId",
                table: "Courses",
                column: "FeatureId",
                principalTable: "CourseFeatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
