namespace APIQuanLyKhachSan.Models.Authentication
{
    public class OutputToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string InvokeToken { get; set; }
        public string Times { get; set; }
        public int TrangThai {  get; set; }
        public string IdNguoiDung {  get; set; }
    }
}
