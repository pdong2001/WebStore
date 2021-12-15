using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class ChiTietSP : Entity
    {
        [DisplayName("Sản phẩm")]
        public int ItemId { get; set; }

        [DisplayName("Sản phẩm")]
        public SanPham SanPham { get; set; }

        [DisplayName("Số lượng")]
        [DefaultValue(0)]
        public double Quantity { get; set; } = 0;

        [DisplayName("Đơn vị tính")]
        public string Unit { get; set; }

        [DisplayName("Giá nhập TB")]
        [DefaultValue(0)]
        public double AverateReceiptPrice { get; set; } = 0;

        [DefaultValue(0)]
        [DisplayName("Đã bán")]
        public double Sold { get; set; } = 0;

        [DisplayName("Giá bán")]
        [DefaultValue(0)]
        public double Price { get; set; } = 0;

        [DisplayName("Tên tùy chọn")]
        public string Name { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }

        [DisplayName("Ảnh")]
        public int? ImageName { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [DisplayName("Ảnh")]
        public IFormFile ImageFile { get; set; }
    }
}
