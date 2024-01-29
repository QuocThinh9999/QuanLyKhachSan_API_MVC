namespace APIQuanLyKhachSan.Models.ThemDuLieu
{
    public class CheckDanhSachPhong
    {
        public DateTime GioCheckin { get; set; }

        public DateTime GioCheckout { get; set; }
        public int? SoNguoiLon { get; set; }

        public int? SoTreEm { get; set; }

        public decimal GiaPhongMin { get; set; }
        public decimal GiaPhongMax { get; set; }
    }
}
