using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public enum TrangThaiHoaDon
    {
        [Display(Name = "Đang yêu cầu")]
        Ordering,
        [Display(Name = "Đã hoàn thành")]
        Completed,
        [Display(Name = "Đang vận chuyển")]
        Shipping,
        [Display(Name = "Đã chấp nhận")]
        Accepted,
        [Display(Name = "Từ chối")]
        Refused
    }
}