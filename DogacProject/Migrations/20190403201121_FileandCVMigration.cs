using Microsoft.EntityFrameworkCore.Migrations;

namespace DogacProject.Migrations
{
    public partial class FileandCVMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CVContent",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVContent",
                table: "Students");
        }
    }
}
