namespace APIQuanLyKhachSan.Models.ModelView
{
    public class CheckPhong
    {
        public NgayKhongKhaDung ngayKhongKhaDung { get; set; }
        public class NgayKhongKhaDung
        {
            public DateTime GioCheckin { get; set; }
            public DateTime GioCheckout { get; set; }
        }
    }
}
