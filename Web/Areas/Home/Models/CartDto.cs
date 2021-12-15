using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Home.Models
{
    public class CartDto
    {
        public string TongTien { get; set; }

        public int SoSanPham { get; set; }

        public string TienSanPhamMoi { get; set; }
    }
}
