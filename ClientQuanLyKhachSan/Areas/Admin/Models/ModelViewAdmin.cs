using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientQuanLyKhachSan.Areas.Admin.Models
{
    public class ModelViewAdmin
    {
        public List<ThongTinWeb> ThongTinWebs { get; set; }
        public class ThongTinWeb
        {
            public string LoaiThongTin { get; set; } = null!;

            public string? MoTa { get; set; }

            public string? UrlImages { get; set; }
            public IFormFile? Images { get; set; }
        }
        public List<HoaDonAdmin> HoaDonAdmins { get; set; }
        public class HoaDonAdmin
        {
            public string IdHoaDon { get; set; }
            public string TenNguoiDung { get; set; }
            public string TenPhong { get; set; }
            public DateTime GioCheckin { get; set; }
            public DateTime GioCheckout { get; set; }
            public decimal TongTien { get; set; }
            public string TrangThai { get; set; }
            public string yeucau { get; set; }
            public string email { get; set; }
            
        }
        public int? ThongBaoHoaDon { get; set; }
        public List<DoanhThu> doanhThus { get; set; }
        public class DoanhThu
        {
            public decimal DoanhThuThang { get; set; }
            public int Thang { get; set; }
            public int Nam { get; set; }
        }
        public List<PhongTrangChu> PhongTrangChus { get; set; }
        public class PhongTrangChu
        {
            public string IdPhong { get; set; }
            public string TenPhong { get; set; }
            public decimal GiaPhong { get; set; }
            public decimal GiaSauGiam { get; set; }
            public int SoNguoiLon { get; set; }
            public int SoTreEm { get; set; }
            public int DienTich { get; set; }
            public string MoTa { get; set; }
            public string UrlImage { get; set; }
        }
        public ChiTietTrangPhong ChiTietTrangPhongs { get; set; }
        public class ChiTietTrangPhong
        {
            public string id { get; set; } = null!;
            public string TenPhong { get; set; }
            public int? SoLuong {  get; set; }
            public decimal GiaPhong { get; set; }
            public decimal GiaSauGiam { get; set; }
            public int? SoNguoiLon { get; set; }
            public int? SoTreEm { get; set; }
            public int? DienTich { get; set; }
            public string MoTa { get; set; }
            public List<string> UrlImages { get; set; }   
        }
        public class CapNhatPhong
        {
            public string? id { get; set; }
            public string TenPhong { get; set; } = null!;
            public int SoLuong { get; set; } = 0!;
            public decimal GiaPhong { get; set; } = 0!;
            public int SoNguoiLon { get; set; } = 0!;
            public int? SoTreEm { get; set; }
            public int DienTich { get; set; } = 0!;
            public string MoTa { get; set; } = null!;
            public string? UrlImage { get; set; }
            public List<string?> UrlImages { get; set; }
            public IFormFileCollection? Images { get; set; }
        }
        public class ThemBaiViet
        {
            public string id {  get; set; }
            public string? UrlImage { get; set; }
            public List<string?> UrlImages { get; set; }
            public IFormFileCollection? Images { get; set; }
            public string? MoTa { get; set; }
            public string? Tieude { get; set; }
        }
        public List<TuVan> tuVan { get; set; }    
        public class TuVan
        {   
            public string? Email { get; set; }
            public DateTime? NgayGioNhan { get; set; }
            public string IdTuVan { get; set; } = null!;
            public string? Ten { get; set; }
            public string? LoiNhan { get; set; }
        }
        public int? ThongBaoTuVan { get; set; }
        public class GiamGia
        { 
            public decimal GiaPhong { get; set; }
            public decimal GiaSauGiam { get; set; }

        }
        public List<TaiKhoanNguoiDung> taiKhoanNguoiDungs { get; set; }
        public class TaiKhoanNguoiDung
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public int? TrangThai { get; set; }
        }
        public List<BlogTrangChu> BlogTrangChus { get; set; }
        public BlogTrangChu blogTrangChu { get; set; }
        public class BlogTrangChu
        {
            public string IdBlog { get; set; }
            public string Tieude { get; set; }
            public string UrlImage { get; set; }
            public string MoTa { get; set; }
        }
    }
}
