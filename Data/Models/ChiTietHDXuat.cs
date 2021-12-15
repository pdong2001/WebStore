using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{
    public class ChiTietHDXuat
    {
        public int MaHDXuat { get; set; }

        public HoaDonXuat HDXuat { get; set; }

        [DisplayName("Tùy chọn")]
        public int MaChiTietSP { get; set; }

        [DisplayName("Tùy chọn")]
        public ChiTietSP ChiTietSP { get; set; }

        [DisplayName("Số lượng")]
        public double SoLuong { get; set; }

        [DisplayName("Giá")]
        public double Gia { get; set; }
    }
}
