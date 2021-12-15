using System;
using System.ComponentModel;

namespace Data.Models
{
    public class LoaiSP : Entity
    {
        [DisplayName("Tên loại")]
        public string Name { get; set; }

        [DisplayName("Số sản phẩm")]
        public int ItemsCount { get; set; }

        [DisplayName("Hiển thị")]
        public bool IsActivated { get; set; }

        [DisplayName("Ghi chú")]
        public string Note { get; set; }
    }
}
