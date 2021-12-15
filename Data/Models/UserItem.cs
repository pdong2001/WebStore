using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class UserItem
    {
        public string UserId { get; set; }
        public int MaChiTietSP { get; set; }
        public ChiTietSP ChiTietSP { get; set; }
        public double SoLuong { get; set; }
    }
}
