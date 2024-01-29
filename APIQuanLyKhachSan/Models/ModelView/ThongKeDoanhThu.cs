namespace APIQuanLyKhachSan.Models.ModelView
{
    public class ThongKeDoanhThu
    {
        public List<DoanhThu> doanhThus { get; set; }
        public class DoanhThu
        {
            public decimal DoanhThuThang { get; set; }
            public int Thang {  get; set; }
            public int Nam { get; set; }
        }
        public List<ThongKePhong> thongKePhongs { get; set; }
        public class ThongKePhong
        {
            public string TenPhong { get; set; }
            public int SoLuong { get; set; }
        }
    }
}
