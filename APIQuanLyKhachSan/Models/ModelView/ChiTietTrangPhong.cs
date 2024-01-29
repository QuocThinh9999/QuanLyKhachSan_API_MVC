namespace APIQuanLyKhachSan.Models.ModelView
{
    public class ChiTietTrangPhong
    {
        public string id { get; set; } = null!;
        public string TenPhong { get; set; }
        public int? SoLuong { get; set; }
        public decimal GiaPhong { get; set; }
        public decimal GiaSauGiam { get; set; }
        public int? SoNguoiLon { get; set; }
        public int? SoTreEm { get; set; }
        public int? DienTich { get; set; }
        public string MoTa { get; set; }
        public List<string> UrlImages { get; set; }
        public List<NgayKhongKhaDung> ngayKhongKhaDung { get; set; }
        public class NgayKhongKhaDung
        {
            public DateTime GioCheckin { get; set; }
            public DateTime GioCheckout { get; set; }
        }
    }
   
}
