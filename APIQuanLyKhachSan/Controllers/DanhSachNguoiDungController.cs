using APIQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIQuanLyKhachSan.Areas.Admin.Models.TaiKhoanNguoiDung;
using WebQuanLyKhachSan.Models.ThemDuLieu;

namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class DanhSachNguoiDungController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _context;
        public DanhSachNguoiDungController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [HttpGet("danh-sach-nguoi-dung")]
        public IActionResult Danhsachnguoidung()
        {
            var taiKhoanNguoiDungs = pvDanhsachnguoidung();
            return Ok(taiKhoanNguoiDungs);
        }
        private List<TaiKhoanNguoiDung> pvDanhsachnguoidung()
        {
            var items = _context.NguoiDungs.Where(c => c.IdQuyen == "User").ToList();

            List<TaiKhoanNguoiDung> taiKhoanNguoiDungs = new List<TaiKhoanNguoiDung>();
            foreach (var item in items)
            {
                var trangthai = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                var tknd = new TaiKhoanNguoiDung();
                tknd.Name = item.Ten;
                tknd.Email = item.Email;
                tknd.Id = item.IdNguoiDung;
                tknd.TrangThai = trangthai.TrangThai;
                taiKhoanNguoiDungs.Add(tknd);
            }
            return taiKhoanNguoiDungs;

        }
        [HttpGet("danh-sach-nguoi-dung/{id}")]
        public IActionResult Danhsachnguoidung(string id)
        {
            var tknd = pvDanhsachnguoidung(id);
            return Ok(tknd);

        }
        private List<TaiKhoanNguoiDung> pvDanhsachnguoidung(string id)
        {
            var item = _context.NguoiDungs.FirstOrDefault(c => c.IdQuyen == "User" && c.IdNguoiDung == id);


            if (item != null)
            {
                var trangthai = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                var tknd = new TaiKhoanNguoiDung();
                tknd.Name = item.Ten;
                tknd.Email = item.Email;
                tknd.Id = item.IdNguoiDung;
                tknd.TrangThai = trangthai.TrangThai;
                List<TaiKhoanNguoiDung> tknds = new List<TaiKhoanNguoiDung>
                {
                    tknd
                };
                return tknds;
            }
            return null;

        }
        [HttpPatch("cap-nhat-tai-khoan/{id}")]
        public IActionResult CapNhatTaiKhoan([FromForm] string cntk)
        {
            var taikhoan=pvCapNhatTaiKhoan(cntk);
            return Ok(taikhoan);
        }
        private XacThuc pvCapNhatTaiKhoan([FromForm] string cntk)
        {
            var input = System.Text.Json.JsonSerializer.Deserialize<TaiKhoanNguoiDung>(cntk);
            var taikhoan = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == input.Id);
            taikhoan.TrangThai = input.TrangThai;
            _context.XacThucs.Update(taikhoan);
            _context.SaveChanges();
            return taikhoan;
        }
    }
}
