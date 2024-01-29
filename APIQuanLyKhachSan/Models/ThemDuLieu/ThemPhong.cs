using System.ComponentModel.DataAnnotations;

namespace WebQuanLyKhachSan.Models.ThemDuLieu
{
	public class ThemPhong
	{
        public string id { get; set; }
        public string TenPhong { get; set; }
		public int SoLuong { get; set; }
		public decimal GiaPhong { get; set; }
		public int SoNguoiLon { get; set; }
		public int SoTreEm { get; set; }
		public int DienTich { get; set; }
		public string MoTa { get; set; }
        public List<string?> UrlImages { get; set; }

    }
}
