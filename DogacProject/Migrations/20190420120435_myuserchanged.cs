using Microsoft.EntityFrameworkCore.Migrations;

namespace DogacProject.Migrations
{
    public partial class myuserchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "departmentManagerId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "departmentManagerId",
                table: "AspNetUsers");
        }
    }
}
