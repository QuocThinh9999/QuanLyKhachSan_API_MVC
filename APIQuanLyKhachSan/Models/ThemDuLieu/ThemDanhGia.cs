using System.ComponentModel.DataAnnotations;

namespace APIQuanLyKhachSan.Models.ThemDuLieu
{
    public class ThemDanhGia
    {
        public string IdHoaDon { get; set; } = null!;
        public string? IdNguoiDung { get; set; }
        public int SoSao { get; set; }

        public string? NhanXet { get; set; }
    }
}
