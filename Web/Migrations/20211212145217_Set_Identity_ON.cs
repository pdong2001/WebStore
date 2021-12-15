using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Set_Identity_ON : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
