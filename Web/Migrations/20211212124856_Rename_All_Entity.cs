using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class Rename_All_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_ItemDetails_ItemDetailId",
                table: "UserItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemDetails_DefaultDetailId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "BillLines");

            migrationBuilder.DropTable(
                name: "ReceiptDetails");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "ItemDetails");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "HoaDonXuat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Commune = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Total = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    ShipFee = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Paid = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonXuat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonXuat_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoaiSP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemsCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiSP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonNhap",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Paid = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonNhap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonNhap_NhaCungCap_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "NhaCungCap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChietTietHDNhap",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    ItemDetailId = table.Column<int>(type: "int", nullable: false),
                    Quanity = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChietTietHDNhap", x => new { x.ReceiptId, x.ItemDetailId });
                    table.ForeignKey(
                        name: "FK_ChietTietHDNhap_HoaDonNhap_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "HoaDonNhap",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDonXuat",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false),
                    ItemDetailId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    OutboundPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDonXuat", x => new { x.BillId, x.ItemDetailId });
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDonXuat_HoaDonXuat_BillId",
                        column: x => x.BillId,
                        principalTable: "HoaDonXuat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ShowAtHome = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    OptionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MainImageName = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    MinPrice = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    MaxPrice = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    OptionCount = table.Column<int>(type: "int", nullable: false),
                    Sold = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPham_LoaiSP_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "LoaiSP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietSP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverateReceiptPrice = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Sold = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietSP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietSP_SanPham_ItemId",
                        column: x => x.ItemId,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChietTietHDNhap_ItemDetailId",
                table: "ChietTietHDNhap",
                column: "ItemDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDonXuat_ItemDetailId",
                table: "ChiTietHoaDonXuat",
                column: "ItemDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietSP_ItemId",
                table: "ChiTietSP",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonNhap_ProviderId",
                table: "HoaDonNhap",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonXuat_UserId",
                table: "HoaDonXuat",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_CategoryId",
                table: "SanPham",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_DefaultDetailId",
                table: "SanPham",
                column: "DefaultDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_ChiTietSP_ItemDetailId",
                table: "UserItems",
                column: "ItemDetailId",
                principalTable: "ChiTietSP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChietTietHDNhap_ChiTietSP_ItemDetailId",
                table: "ChietTietHDNhap",
                column: "ItemDetailId",
                principalTable: "ChiTietSP",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDonXuat_ChiTietSP_ItemDetailId",
                table: "ChiTietHoaDonXuat",
                column: "ItemDetailId",
                principalTable: "ChiTietSP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SanPham_ChiTietSP_DefaultDetailId",
                table: "SanPham",
                column: "DefaultDetailId",
                principalTable: "ChiTietSP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserItems_ChiTietSP_ItemDetailId",
                table: "UserItems");

            migrationBuilder.DropForeignKey(
                name: "FK_SanPham_ChiTietSP_DefaultDetailId",
                table: "SanPham");

            migrationBuilder.DropTable(
                name: "ChietTietHDNhap");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDonXuat");

            migrationBuilder.DropTable(
                name: "HoaDonNhap");

            migrationBuilder.DropTable(
                name: "HoaDonXuat");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "ChiTietSP");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "LoaiSP");

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commune = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Paid = table.Column<double>(type: "float", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShipFee = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Total = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    ItemsCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paid = table.Column<double>(type: "float", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BillLines",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false),
                    ItemDetailId = table.Column<int>(type: "int", nullable: false),
                    OutboundPrice = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillLines", x => new { x.BillId, x.ItemDetailId });
                    table.ForeignKey(
                        name: "FK_BillLines_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    DefaultDetailId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainImageName = table.Column<int>(type: "int", nullable: true),
                    MaxPrice = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    MinPrice = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OptionCount = table.Column<int>(type: "int", nullable: false),
                    OptionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ShowAtHome = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Sold = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItemDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AverateReceiptPrice = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    ImageName = table.Column<int>(type: "int", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Sold = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDetails_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetails",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    ItemDetailId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quanity = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetails", x => new { x.ReceiptId, x.ItemDetailId });
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_ItemDetails_ItemDetailId",
                        column: x => x.ItemDetailId,
                        principalTable: "ItemDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillLines_ItemDetailId",
                table: "BillLines",
                column: "ItemDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDetails_ItemId",
                table: "ItemDetails",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_DefaultDetailId",
                table: "Items",
                column: "DefaultDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_ItemDetailId",
                table: "ReceiptDetails",
                column: "ItemDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ProviderId",
                table: "Receipts",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserItems_ItemDetails_ItemDetailId",
                table: "UserItems",
                column: "ItemDetailId",
                principalTable: "ItemDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillLines_ItemDetails_ItemDetailId",
                table: "BillLines",
                column: "ItemDetailId",
                principalTable: "ItemDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemDetails_DefaultDetailId",
                table: "Items",
                column: "DefaultDetailId",
                principalTable: "ItemDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
