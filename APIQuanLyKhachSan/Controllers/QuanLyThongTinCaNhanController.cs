using APIQuanLyKhachSan.Models;
using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class QuanLyThongTinCaNhanController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _dbContext;
        public QuanLyThongTinCaNhanController(DbQuanLyKhachSanContext context)
        {
            _dbContext = context;
        }
        [HttpGet("xem-thong-tin-ca-nhan/{id}")]
        public IActionResult CapNhatThongTinCaNhan(string id)
        {

            var nguoidung = _dbContext.NguoiDungs.FirstOrDefault(c => c.IdQuyen == "User" && c.IdNguoiDung == id);

            return Ok(nguoidung);
        }
        [HttpPost("cap-nhat-thong-tin-ca-nhan/{id}")]
        public IActionResult PostCapNhatThongTinCaNhan([FromForm] string ttcn) 
        {
            pvPostCapNhatThongTinCaNhan(ttcn);
            return Ok(); 
        }
        private void pvPostCapNhatThongTinCaNhan([FromForm] string ttcn)
        {
            var nguoiDung = System.Text.Json.JsonSerializer.Deserialize<NguoiDung>(ttcn);
            var nd = _dbContext.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == nguoiDung.IdNguoiDung);
            nd.Ten = nguoiDung.Ten;
            nd.SoDienThoai = nguoiDung.SoDienThoai;
            nd.NgaySinh = nguoiDung.NgaySinh;
            nd.GioiTinh = nguoiDung.GioiTinh;
            _dbContext.Update(nd);
            _dbContext.SaveChanges();
            
        }
    }
}
