using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class HoaDonXuat : Entity
    {
        [DisplayName("Tên khách hàng")]
        public string UserId { get; set; }

        [DisplayName("Tên khách hàng")]
        public AppUser User { get; set; }

        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }

        [Required]
        [DisplayName("Tỉnh")]
        public string Tinh { get; set; }

        [Required]
        [DisplayName("Huyện")]
        public string Huyen { get; set; }

        [Required]
        [DisplayName("Xã")]
        public string Xa { get; set; }

        [DisplayName("Trạng thái")]
        public TrangThaiHoaDon Status { get; set; }

        [DisplayName("Tổng tiền")]
        public double TongTien { get; set; }

        [DisplayName("Phí giao hàng")]
        public double PhiShip { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }

        [Required]
        [MinLength(10)]
        [Phone]
        [DisplayName("Sô điện thoại")]
        public string SoDienThoai { get; set; }

        [EmailAddress]
        [DisplayName("Email")]
        public string Email { get; set; }

        public ICollection<ChiTietHDXuat> Lines { get; set; }
    }
}
