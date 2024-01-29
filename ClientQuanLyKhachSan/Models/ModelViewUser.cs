using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientQuanLyKhachSan.Models
{
    public class ModelViewUser
    {
        public List<ThongTinWeb> ThongTinWebs { get; set; }
        public class ThongTinWeb
        {
            public string LoaiThongTin { get; set; } = null!;

            public string? MoTa { get; set; }

            public string? UrlImages { get; set; }
        }
        public List<PhongTrangChu> PhongTrangChus { get; set; }
        public class PhongTrangChu
        {
            public string IdPhong { get; set; }
            public string TenPhong { get; set; }
            public decimal GiaPhong { get; set; }
            public decimal GiaSauGiam { get; set; }
            public int SoNguoiLon {  get; set; }
            public int SoTreEm { get; set; }
            public int DienTich {  get; set; }
            public string MoTa {  get; set; }
            public string UrlImage {  get; set; }
        }
        public List<BlogTrangChu> BlogTrangChus { get; set; }
        public class BlogTrangChu
        {
            public string IdBlog { get; set; }
            public string Tieude { get; set; }
            public string UrlImage { get; set; }
        }
        public ChiTietBlog ChiTietBlogs { get; set; }
        public class ChiTietBlog
        {
            public string IdBlog { get; set; } = null!;

            public List<string> UrlImages { get; set; }

            public string? MoTa { get; set; }

            public string? Tieude { get; set; }
        }
        public InputDangKy InputDangKys { get; set; }
        public class InputDangKy
        {
            public string Email { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Password2 { get; set; }
        }
        public InputOTP inputOTP { get; set; }
        public class InputOTP
        {
            public string IdNguoiDung { get; set; } = null!;
            public string? MaXacThuc { get; set; }
            public int? QuenMatKhau { get; set; }
        }
        public NguoiDungDangKy nguoiDungDangKy { get; set; }
        public class NguoiDungDangKy
        {
            public string IdNguoiDung { get; set; } = null!;

            public int? TrangThai { get; set; }
        }
        public InputQMK inputQMK { get; set; }
        public class InputQMK
        {
            public string email { get; set; }
            public string IdNguoiDung { get; set; } = null!;
            public int TrangThai { get; set; }
            public string? MaXacThuc { get; set; }
        }
        public DoiLaiMatKhau doiLaiMatKhau { get; set; }
        public class DoiLaiMatKhau
        {
            public string IdNguoiDung { get; set; } = null!;
            public string Password { get; set; }
            public string Password2 { get; set; }
        }
        public string email {  get; set; }

        public ChiTietTrangPhong ChiTietTrangPhongs { get; set; }
        public class ChiTietTrangPhong
        {
            public string id { get; set; } = null!;
            public string TenPhong { get; set; }
           
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
        public NguoiDung nguoiDung {  get; set; }
        public int TrangThaiCapNhat {  get; set; }
        public int TrangThaiTuVan {  get; set; }
        public class NguoiDung
        {
            public string IdNguoiDung { get; set; } = null!;
            public string? IdQuyen { get; set; }
            public string? Ten { get; set; }
            public string? SoDienThoai { get; set; }
            public string? Email { get; set; }
            public string? MatKhau { get; set; }
            public string? GioiTinh { get; set; }
            public DateTime? NgaySinh { get; set; }
            public string? AnhDaiDien { get; set; }

        }
        public List<LichSuDatPhong> lichSuDatPhongs { get; set; }
        public class LichSuDatPhong
        {
            public string IdHoaDon { get; set; } = null!;
            public string? IdNguoiDung { get; set; }
            public string? IdPhong { get; set; }
            public string? TenPhong { get; set; }
            public int? SoTreEm { get; set; }
            public int? SoNguoiLon { get; set; }
            public DateTime GioCheckin { get; set; }
            public DateTime GioCheckout { get; set; }
            public decimal? PhuThu { get; set; }
            public decimal? Vat { get; set; }
            public decimal TongTien { get; set; }
            public string? TrangThai { get; set; }
            public string? YeuCau { get; set; }
            public string? UrlImages { get; set; }
        }
        public LichSuChiTiet lichSuChiTiet { get; set; }
        public class LichSuChiTiet
        {
            public string IdHoaDon { get; set; } = null!;
            public string? IdPhong { get; set; }
            public string? TenPhong { get; set; }
            public int? SoTreEm { get; set; }
            public int? SoNguoiLon { get; set; }
            public DateTime GioCheckin { get; set; }
            public DateTime GioCheckout { get; set; }
            public int TongNgay { get; set; }
            public decimal TongTien { get; set; }
            public string? TrangThai { get; set; }
            public string? YeuCau { get; set; }
            public string? UrlImages { get; set; }
            public string? TenNguoiDung { get; set; }
            public string? EmailNguoiDung { get; set; }
            public string? SDTNguoiDung { get; set; }
        }
        public DanhSachCheckPhong danhSachCheckPhong { get; set; }
        public class DanhSachCheckPhong
        {
            public DateTime GioCheckin { get; set; }

            public DateTime GioCheckout { get; set; }
            public int? SoNguoiLon { get; set; }

            public int? SoTreEm { get; set; }

            public decimal GiaPhongMin { get; set; }
            public decimal GiaPhongMax { get; set; }
            public string Phongs {  get; set; }
        }
        public TaoHoaDon taoHoaDon { get; set; }
        public class TaoHoaDon
        {
            public string IdNguoiDung { get; set; }
            public string IdPhong { get; set; }
            public DateTime GioCheckin { get; set; }

            public DateTime GioCheckout { get; set; }
            public string? yeucau { get; set; }
        }
        public List<NgayKhongKhaDung> ngayKhongKhaDung { get; set; }
        public class NgayKhongKhaDung
        {
            public DateTime GioCheckin { get; set; }
            public DateTime GioCheckout { get; set; }
        }
        public List<DateTime> nkkd { get; set; }
        public string JonNkkd {  get; set; }
    }
}
