using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebQuanLyKhachSan.Models.ThemDuLieu
{
	public class ThemLoaiPhong
	{
		[Required(ErrorMessage = "Vui lòng nhập tên loại phòng!")]
		public string TenLoaiPhong { get; set; }
	}
}
