using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{
    public class ThongTinWeb : Entity
    {
        [DisplayName("Tên thương hiệu")]
        [Required]
        public string BrandName { get; set; }

        [DisplayName("Nội dung slider")]
        public string HeroContent { get; set; }

        [DisplayName("Ảnh slider 1")]
        public int? HeroImageOneName { get; set; }

        [DisplayName("Ảnh slider 2")]
        public int? HeroImageTwoName { get; set; }

        [NotMapped]
        [DisplayName("Ảnh slider")]
        [DataType(DataType.Upload)]
        public IFormFileCollection SliderFiles { get; set; }

        [NotMapped]
        [DisplayName("Ảnh banner")]
        [DataType(DataType.Upload)]
        public IFormFileCollection BannerFiles { get; set; }

        [DisplayName("Nội dung banner 1")]
        public string BannerOneContent { get; set; }

        [DisplayName("Ảnh banner 1")]
        public int? BannerOneImageName { get; set; }

        [DisplayName("Nội dung banner 2")]
        public string BannerTwoContent { get; set; }

        [DisplayName("Ảnh banner 2")]
        public int? BannerTwoImageName { get; set; }

        [DisplayName("Nội dung banner 3")]
        public string BannerThreeContent { get; set; }

        [DisplayName("Ảnh banner 3")]
        public int? BannerThreeImageName { get; set; }

        [DisplayName("Nội dung mới")]
        public string LastestCaption { get; set; }

        [DisplayName("Nội dung bộ sưu tập")]
        public string GalleryContent { get; set; }

        [DisplayName("Nội dung thư mời")]
        public string Letters { get; set; }

        [DisplayName("Đường dẫn Pinterest")]
        [DataType(DataType.Url)]
        public string PinterestLink { get; set; }

        [DisplayName("Đường dẫn Facebook")]
        [DataType(DataType.Url)]
        public string FacebookLink { get; set; }

        [DisplayName("Đường dẫn Instagram")]
        [DataType(DataType.Url)]
        public string InstagramLink { get; set; }

        [DisplayName("Đường dẫn Twitter")]
        [DataType(DataType.Url)]
        public string TwitterLink { get; set; }

        [DisplayName("Đang kích hoạt")]
        public bool IsActivated { get; set; } = false;
    }
}
