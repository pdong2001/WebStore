using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Renam_Properties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChietTietHDNhap_ChiTietSP_ItemDetailId",
                table: "ChietTietHDNhap");

            migrationBuilder.DropForeignKey(
                name: "FK_ChietTietHDNhap_HoaDonNhap_ReceiptId",
                table: "ChietTietHDNhap");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDonXuat_ChiTietSP_ItemDetailId",
                table: "ChiTietHoaDonXuat");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDonXuat_HoaDonXuat_BillId",
                table: "ChiTietHoaDonXuat");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_ChiTietSP_ItemDetailId",
                table: "UserItems");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "UserItems",
                newName: "SoLuong");

            migrationBuilder.RenameColumn(
                name: "ItemDetailId",
                table: "UserItems",
                newName: "MaChiTietSP");

            migrationBuilder.RenameIndex(
                name: "IX_UserItems_ItemDetailId",
                table: "UserItems",
                newName: "IX_UserItems_MaChiTietSP");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ChiTietHoaDonXuat",
                newName: "SoLuong");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ChiTietHoaDonXuat",
                newName: "Gia");

            migrationBuilder.RenameColumn(
                name: "ItemDetailId",
                table: "ChiTietHoaDonXuat",
                newName: "MaChiTietSP");

            migrationBuilder.RenameColumn(
                name: "BillId",
                table: "ChiTietHoaDonXuat",
                newName: "MaHDXuat");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDonXuat_ItemDetailId",
                table: "ChiTietHoaDonXuat",
                newName: "IX_ChiTietHoaDonXuat_MaChiTietSP");

            migrationBuilder.RenameColumn(
                name: "Quanity",
                table: "ChietTietHDNhap",
                newName: "SoLuong");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ChietTietHDNhap",
                newName: "Gia");

            migrationBuilder.RenameColumn(
                name: "ItemDetailId",
                table: "ChietTietHDNhap",
                newName: "MaChiTietSP");

            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                table: "ChietTietHDNhap",
                newName: "HDNhapId");

            migrationBuilder.RenameIndex(
                name: "IX_ChietTietHDNhap_ItemDetailId",
                table: "ChietTietHDNhap",
                newName: "IX_ChietTietHDNhap_MaChiTietSP");

            migrationBuilder.AddColumn<int>(
                name: "HDXuatId",
                table: "ChiTietHoaDonXuat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDonXuat_HDXuatId",
                table: "ChiTietHoaDonXuat",
                column: "HDXuatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTietHDNhap_ChiTietSP_MaChiTietSP",
                table: "ChietTietHDNhap",
                column: "MaChiTietSP",
                principalTable: "ChiTietSP",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTietHDNhap_HoaDonNhap_HDNhapId",
                table: "ChietTietHDNhap",
                column: "HDNhapId",
                principalTable: "HoaDonNhap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDonXuat_ChiTietSP_MaChiTietSP",
                table: "ChiTietHoaDonXuat",
                column: "MaChiTietSP",
                principalTable: "ChiTietSP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDonXuat_HoaDonXuat_HDXuatId",
                table: "ChiTietHoaDonXuat",
                column: "HDXuatId",
                principalTable: "HoaDonXuat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDonXuat_HoaDonXuat_MaHDXuat",
                table: "ChiTietHoaDonXuat",
                column: "MaHDXuat",
                principalTable: "HoaDonXuat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_ChiTietSP_MaChiTietSP",
                table: "UserItems",
                column: "MaChiTietSP",
                principalTable: "ChiTietSP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChietTietHDNhap_ChiTietSP_MaChiTietSP",
                table: "ChietTietHDNhap");

            migrationBuilder.DropForeignKey(
                name: "FK_ChietTietHDNhap_HoaDonNhap_HDNhapId",
                table: "ChietTietHDNhap");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDonXuat_ChiTietSP_MaChiTietSP",
                table: "ChiTietHoaDonXuat");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDonXuat_HoaDonXuat_HDXuatId",
                table: "ChiTietHoaDonXuat");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDonXuat_HoaDonXuat_MaHDXuat",
                table: "ChiTietHoaDonXuat");

            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_ChiTietSP_MaChiTietSP",
                table: "UserItems");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietHoaDonXuat_HDXuatId",
                table: "ChiTietHoaDonXuat");

            migrationBuilder.DropColumn(
                name: "HDXuatId",
                table: "ChiTietHoaDonXuat");

            migrationBuilder.RenameColumn(
                name: "SoLuong",
                table: "UserItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "MaChiTietSP",
                table: "UserItems",
                newName: "ItemDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_UserItems_MaChiTietSP",
                table: "UserItems",
                newName: "IX_UserItems_ItemDetailId");

            migrationBuilder.RenameColumn(
                name: "SoLuong",
                table: "ChiTietHoaDonXuat",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Gia",
                table: "ChiTietHoaDonXuat",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "MaChiTietSP",
                table: "ChiTietHoaDonXuat",
                newName: "ItemDetailId");

            migrationBuilder.RenameColumn(
                name: "MaHDXuat",
                table: "ChiTietHoaDonXuat",
                newName: "BillId");

            migrationBuilder.RenameIndex(
                name: "IX_ChiTietHoaDonXuat_MaChiTietSP",
                table: "ChiTietHoaDonXuat",
                newName: "IX_ChiTietHoaDonXuat_ItemDetailId");

            migrationBuilder.RenameColumn(
                name: "SoLuong",
                table: "ChietTietHDNhap",
                newName: "Quanity");

            migrationBuilder.RenameColumn(
                name: "Gia",
                table: "ChietTietHDNhap",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "MaChiTietSP",
                table: "ChietTietHDNhap",
                newName: "ItemDetailId");

            migrationBuilder.RenameColumn(
                name: "HDNhapId",
                table: "ChietTietHDNhap",
                newName: "ReceiptId");

            migrationBuilder.RenameIndex(
                name: "IX_ChietTietHDNhap_MaChiTietSP",
                table: "ChietTietHDNhap",
                newName: "IX_ChietTietHDNhap_ItemDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTietHDNhap_ChiTietSP_ItemDetailId",
                table: "ChietTietHDNhap",
                column: "ItemDetailId",
                principalTable: "ChiTietSP",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTietHDNhap_HoaDonNhap_ReceiptId",
                table: "ChietTietHDNhap",
                column: "ReceiptId",
                principalTable: "HoaDonNhap",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDonXuat_ChiTietSP_ItemDetailId",
                table: "ChiTietHoaDonXuat",
                column: "ItemDetailId",
                principalTable: "ChiTietSP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDonXuat_HoaDonXuat_BillId",
                table: "ChiTietHoaDonXuat",
                column: "BillId",
                principalTable: "HoaDonXuat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_ChiTietSP_ItemDetailId",
                table: "UserItems",
                column: "ItemDetailId",
                principalTable: "ChiTietSP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
