namespace APIQuanLyKhachSan.Models.ThemDuLieu
{
    public class TaoHoaDon
    {
        public string IdNguoiDung {  get; set; }
        public string IdPhong { get; set; }
        public DateTime GioCheckin { get; set; }

        public DateTime GioCheckout { get; set; }
        public string? yeucau { get; set; }
    }
}
