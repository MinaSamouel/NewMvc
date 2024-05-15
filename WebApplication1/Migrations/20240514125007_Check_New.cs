using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Check_New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Student_Name",
                table: "Students",
                type: "nvarchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "mvarchar(60)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "mvarchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Course_Name",
                table: "Courses",
                type: "nvarchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "mvarchar(60)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Student_Name",
                table: "Students",
                type: "mvarchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "mvarchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Course_Name",
                table: "Courses",
                type: "mvarchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)");
        }
    }
}
