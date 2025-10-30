using System.ComponentModel.DataAnnotations;

namespace de_thi_mau_lap_trinh_web.Models
{
    public class CreateProductRequest
    {
        [Display(Name = "MaLoai")]
        [Required(ErrorMessage = "Mã loại không được để trống")]
        public int MaLoai { get; set; }

        [Display(Name = "TenHang")]
        [Required(ErrorMessage = "Tên hàng không được để trống")]
        public string TenHang { get; set; } = null!;

        [Display(Name = "Gia")]
        [Range(100, 5000, ErrorMessage = "Giá trị phải nằm trong khoảng từ 100 đến 5000.")]
        [Required(ErrorMessage = "Giá không được để trống")]
        public decimal Gia { get; set; }

        [Display(Name = "Anh")]
        [Required(ErrorMessage = "Ảnh không được để trống")]
        [FileExtensions(new string[] { ".jpg", ".png", ".gif", ".tiff" })]
        public IFormFile Anh { get; set; }
    }
}
