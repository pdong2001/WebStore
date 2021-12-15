using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Added_Properties_HoaDonXuat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HoaDonXuat",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoDienThoai",
                table: "HoaDonXuat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "HoaDonXuat");

            migrationBuilder.DropColumn(
                name: "SoDienThoai",
                table: "HoaDonXuat");
        }
    }
}
