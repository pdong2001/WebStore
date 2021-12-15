using System;
using System.Collections.Generic;


namespace Data.Models
{
    public class HoaDonNhap : Entity
    {
        public int? ProviderId { get; set; }

        public double Total { get; set; }

        public double Paid { get; set; }

        public string Note { get; set; }

        public ICollection<ChiTietHDNhap> Details { get; set; }
    }
}
