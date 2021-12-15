using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Data.Models;
using System;
using Microsoft.AspNetCore.Identity;

namespace Data
{
    public class WebStoreDbContext : IdentityDbContext<AppUser>
    {
        public WebStoreDbContext(DbContextOptions<WebStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<LoaiSP> LoaiSP { get; set; }
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<ChiTietSP> ChiTietSP { get; set; }
        public DbSet<ThongTinWeb> WebInfos { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<HoaDonXuat> HoaDonXuat { get; set; }
        public DbSet<ChiTietHDXuat> ChiTietHoaDonXuat { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<HoaDonNhap> HoaDonNhap { get; set; }
        public DbSet<ChiTietHDNhap> ChietTietHDNhap { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            #region Entity of ecommerce modules
            builder.Entity<ThongTinWeb>(webInfo =>
            {
                webInfo.Property(info => info.BrandName).HasMaxLength(30).IsRequired();
                webInfo.Property(info => info.IsActivated).HasDefaultValue(false);
            });
            builder.Entity<LoaiSP>(category =>
            {
                category.Property(c => c.Name).HasMaxLength(50).IsRequired();
                category.Property(c => c.Note).HasMaxLength(512);
                category.Property(c => c.ItemsCount).IsRequired().HasDefaultValue<int>(0);
            });
            builder.Entity<SanPham>(sanPham =>
            {
                sanPham.Property(i => i.Name).HasMaxLength(100).IsRequired();
                sanPham.Property(i => i.ShowAtHome).HasDefaultValue(false);
                sanPham.Property(i => i.OptionName).HasMaxLength(100);
                sanPham.HasOne<LoaiSP>(i => i.Category).WithMany().HasForeignKey(i => i.CategoryId)
                .IsRequired(false).OnDelete(DeleteBehavior.SetNull);
                sanPham.Property(i => i.MinPrice).IsRequired().HasDefaultValue(0);
                sanPham.Property(i => i.MaxPrice).IsRequired().HasDefaultValue(0);
                sanPham.HasOne<ChiTietSP>().WithMany().HasForeignKey(i => i.DefaultDetailId).IsRequired(false);
                sanPham.HasMany<ChiTietSP>(i => i.Details).WithOne(ct => ct.SanPham);
            });
            builder.Entity<ChiTietSP>(chiTietSP =>
            {
                chiTietSP.Property(detail => detail.Name).HasMaxLength(50).IsRequired();
                chiTietSP.Property(detail => detail.AverateReceiptPrice).HasDefaultValue(0).IsRequired();
                chiTietSP.Property(detail => detail.Price).HasDefaultValue(0).IsRequired();
                chiTietSP.HasOne<SanPham>(ct => ct.SanPham).WithMany(i => i.Details).HasForeignKey(detail => detail.ItemId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<HoaDonXuat>(bill =>
            {
                bill.Property(b => b.Tinh).HasMaxLength(100).IsRequired();
                bill.Property(b => b.Huyen).HasMaxLength(100).IsRequired();
                bill.Property(b => b.Xa).HasMaxLength(100).IsRequired();
                bill.Property(b => b.Status).HasDefaultValue(TrangThaiHoaDon.Ordering).IsRequired();
                bill.HasOne<AppUser>(hd => hd.User).WithMany()
                .HasForeignKey(item => item.UserId).IsRequired();
                bill.Property(b => b.TongTien).HasDefaultValue(0).IsRequired();
                bill.Property(b => b.PhiShip).HasDefaultValue(0).IsRequired();
            });
            builder.Entity<ChiTietHDXuat>(billLine =>
            {
                billLine.HasOne<ChiTietSP>(c => c.ChiTietSP).WithMany().HasForeignKey(bl => bl.MaChiTietSP).IsRequired();
                billLine.HasOne<HoaDonXuat>().WithMany(b => b.Lines).HasForeignKey(bl => bl.MaHDXuat).IsRequired();
                billLine.HasKey(bl => new { bl.MaHDXuat, bl.MaChiTietSP });
            });
            builder.Entity<UserItem>(cart =>
            {
                cart.HasKey(c => new { c.UserId, c.MaChiTietSP });
                cart.HasOne<ChiTietSP>(ui => ui.ChiTietSP).WithMany().HasForeignKey(c => c.MaChiTietSP)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
                cart.HasOne<AppUser>().WithMany().HasForeignKey(c => c.UserId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<HoaDonNhap>(receipt =>
            {
                receipt.HasOne<NhaCungCap>().WithMany().HasForeignKey(r => r.ProviderId)
                .IsRequired(false).OnDelete(DeleteBehavior.SetNull);
            });
            builder.Entity<ChiTietHDNhap>(receipt =>
            {
                receipt.HasOne<HoaDonNhap>().WithMany(r => r.Details).HasForeignKey(r => r.HDNhapId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
                receipt.HasOne<ChiTietSP>().WithMany().HasForeignKey(r => r.MaChiTietSP)
                .IsRequired().OnDelete(DeleteBehavior.NoAction);
                receipt.HasKey(r => new { r.HDNhapId, r.MaChiTietSP });
            });
            builder.Entity<Blob>(blob =>
            {
                blob.Property(b => b.Name).IsRequired();
            });
            #endregion
        }
    }
}
