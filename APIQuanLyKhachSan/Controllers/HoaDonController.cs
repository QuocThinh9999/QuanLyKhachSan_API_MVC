using APIQuanLyKhachSan.Models;
using APIQuanLyKhachSan.Models.ModelView;
using APIQuanLyKhachSan.Models.ModelView.ModerViewAdmin;
using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _context;
        public HoaDonController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [HttpGet("danh-sach-hoa-don")]
        [Authorize(Roles = "Admin")]
        public IActionResult DanhSachHoaDon()
        {

            var hoadon = pvDanhSachHoaDon();
            return Ok(hoadon);
        }
        private List<DanhSachHoaDon.HoaDonAdmin> pvDanhSachHoaDon()
        {

            var hoadon = new DanhSachHoaDon();
            hoadon.HoaDonAdmins = new List<DanhSachHoaDon.HoaDonAdmin>();
            var hd = _context.HoaDons.ToList();
            var nd = _context.NguoiDungs.ToList();
            foreach (var item in hd)
            {
                var ndhd = nd.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                var hdhd = new DanhSachHoaDon.HoaDonAdmin();
                hdhd.IdHoaDon = item.IdHoaDon;
                hdhd.TenPhong = item.TenPhong;
                hdhd.TenNguoiDung = ndhd.Ten;
                hdhd.TrangThai = item.TrangThai;
                hdhd.GioCheckin = item.GioCheckin;
                hdhd.GioCheckout = item.GioCheckout;
                hdhd.email = ndhd.Email;
                hdhd.yeucau = item.YeuCau;
                hdhd.TongTien = item.TongTien;
                hoadon.HoaDonAdmins.Add(hdhd);
            }
            hoadon.HoaDonAdmins = hoadon.HoaDonAdmins.OrderBy(tv => tv.TrangThai == "Chờ nhận phòng" ? 0 : 1)
                       .ThenBy(tv => tv.GioCheckin)
                       .ToList();
            var tb = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "hoadon");
            tb.Tttb = 0;
            _context.Update(tb);
            _context.SaveChanges();
            return hoadon.HoaDonAdmins;
        }
        [HttpGet("lich-su-dat-phong/{id}")]
        public IActionResult LichSuDatPhong(string id)
        {
            var hoadon = _context.HoaDons.Where(c=>c.IdNguoiDung==id).OrderByDescending(tv => tv.GioCheckin).ToList();
            return Ok(hoadon);
        }
        
      
        [HttpPost("tao-hoa-don")]
        public IActionResult TaoHoaDon([FromForm] string taohoadon)
        {
            pvTaoHoaDon(taohoadon);
            return Ok();
        }
        private void  pvTaoHoaDon([FromForm] string taohoadon)
        {
            var input = System.Text.Json.JsonSerializer.Deserialize<TaoHoaDon>(taohoadon);
            var thongbao = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "hoadon");
            thongbao.Tttb++;
            var phong = _context.Phongs.FirstOrDefault(c => c.Id == input.IdPhong);
            var ctp = _context.ChiTietPhongs.FirstOrDefault(c => c.IdPhong == input.IdPhong);
            var hoadon = new HoaDon();
            hoadon.IdHoaDon = Guid.NewGuid().ToString();
            hoadon.IdNguoiDung = input.IdNguoiDung;
            hoadon.IdPhong = input.IdPhong;
            hoadon.TenPhong = phong.TenPhong;
            var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phong.UrlImage);
            hoadon.UrlImages = ListUrl[0].UrlImage;
            hoadon.SoTreEm = ctp.SoTreEm;
            hoadon.SoNguoiLon = ctp.SoNguoiLon;
            hoadon.GioCheckin = input.GioCheckin;
            hoadon.GioCheckout = input.GioCheckout;
            hoadon.PhuThu = 0;
            hoadon.Vat = 0.08m;
            hoadon.TongTien = (int)(input.GioCheckout - input.GioCheckin).TotalDays * phong.GiaSauGiam * 1.08m;
            hoadon.TrangThai = "Chờ nhận phòng";
            hoadon.YeuCau = input.yeucau;
            _context.ThongBaos.Update(thongbao);
            _context.HoaDons.Add(hoadon);
            _context.SaveChanges();
            
        }
        [HttpPatch("doi-trang-thai-hoa-don/{id}")]
        public IActionResult DoiTrangThai([FromForm] string id, [FromForm] string trangthai)
        {
            var hoadon = _context.HoaDons.FirstOrDefault(c => c.IdHoaDon == id);
            if (hoadon != null)
            {
                if(trangthai=="Đã hủy")
                {
                    hoadon.TrangThai = "Đã hủy";
                    _context.Update(hoadon);
                    _context.SaveChanges();
                    return Ok();
                }
                if(trangthai=="Thành công")
                {
                    hoadon.TrangThai = "Thành công";
                    _context.Update(hoadon);
                    _context.SaveChanges();
                    return Ok();
                }
                else
                {
                    hoadon.TrangThai = "Chờ nhận phòng";
                    _context.Update(hoadon);
                    _context.SaveChanges();
                    return Ok();
                }
                
            }
            else return BadRequest();
        }
        
        [HttpGet("thong-bao-hoa-don")]
       
        public IActionResult ThongBaoTuVan()
        {
            var tb = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "hoadon");
            return Ok(tb.Tttb);
        }
    }
}

