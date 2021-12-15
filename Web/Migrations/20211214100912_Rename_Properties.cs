using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Rename_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "HoaDonXuat");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "HoaDonXuat",
                newName: "TongTien");

            migrationBuilder.RenameColumn(
                name: "ShipFee",
                table: "HoaDonXuat",
                newName: "PhiShip");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "HoaDonXuat",
                newName: "Xa");

            migrationBuilder.RenameColumn(
                name: "District",
                table: "HoaDonXuat",
                newName: "Tinh");

            migrationBuilder.RenameColumn(
                name: "Commune",
                table: "HoaDonXuat",
                newName: "Huyen");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "HoaDonXuat",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "OutboundPrice",
                table: "ChiTietHoaDonXuat",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "HoaDonXuat",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "HoaDonXuat");

            migrationBuilder.RenameColumn(
                name: "Xa",
                table: "HoaDonXuat",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "TongTien",
                table: "HoaDonXuat",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "Tinh",
                table: "HoaDonXuat",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "PhiShip",
                table: "HoaDonXuat",
                newName: "ShipFee");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "HoaDonXuat",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "Huyen",
                table: "HoaDonXuat",
                newName: "Commune");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ChiTietHoaDonXuat",
                newName: "OutboundPrice");

            migrationBuilder.AddColumn<double>(
                name: "Paid",
                table: "HoaDonXuat",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
