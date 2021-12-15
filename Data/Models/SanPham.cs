using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class SanPham : Entity
    {
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }

        [DisplayName("Loại sản phẩm")]
        public int? CategoryId { get; set; }

        [DisplayName("Loại sản phẩm")]
        public LoaiSP Category { get; set; }

        [DisplayName("Hiển thị")]
        public bool ShowAtHome { get; set; }

        [DisplayName("Tên tùy chọn")]
        public string OptionName { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [DisplayName("Ảnh chính")]
        public IFormFile MainImageFile { get; set; }

        [DisplayName("Ảnh chính")]
        public int? MainImageName { get; set; }

        [DisplayName("Số lượng")]
        public double Quantity { get; set; }

        [DisplayName("Giá tối thiểu")]
        [DataType(DataType.Currency)]
        public double MinPrice { get; set; }

        [DisplayName("Giá tối đa")]
        [DataType(DataType.Currency)]
        public double MaxPrice { get; set; }

        [DisplayName("Số tùy chọn")]
        public int OptionCount { get; set; }

        [DisplayName("Đã bán")]
        public double Sold { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Từ khóa (Ngăn cách bởi \";\")")]
        public string KeyWord { get; set; }

        [DisplayName("Tùy chọn mặc định")]
        public int? DefaultDetailId { get; set; }

        [DisplayName("Danh sách chi tiết")]
        public IList<ChiTietSP> Details { get; set; }
    }
}
