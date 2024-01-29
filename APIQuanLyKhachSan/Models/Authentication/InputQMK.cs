namespace APIQuanLyKhachSan.Models.Authentication
{
    public class InputQMK
    {
        public string email { get;set; }
        public string IdNguoiDung { get; set; } = null!;
        public int TrangThai {  get; set; }
        public string? MaXacThuc { get; set; }
    }
}
