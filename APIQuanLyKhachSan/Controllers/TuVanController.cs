using APIQuanLyKhachSan.Models;
using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQuanLyKhachSan.Models.ThemDuLieu;

namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TuVanController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _context;
        public TuVanController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [HttpGet("danh-sach-tu-van")]
        [Authorize(Roles = "Admin")]
        public IActionResult Danhsachemail()
        {
            var tuvans = pvDanhsachemail();
            return Ok(tuvans);
        }
        private List<TuVan> pvDanhsachemail()
        {
            var tuvans = _context.TuVans.OrderByDescending(tv => tv.NgayGioNhan).ToList();
            var tb = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "email");
            tb.Tttb = 0;
            _context.Update(tb);
            _context.SaveChanges();
            return tuvans;
        }
        [HttpPost("them-tu-van")]
        public IActionResult ThemTuVan([FromForm] ThemTuVan input)
        {
            var item = pvThemTuVan(input);
            return Ok(item);
        }
        private TuVan pvThemTuVan([FromForm] ThemTuVan input)
        {
            if (ModelState.IsValid)
            {
                var tuvan = new TuVan();
                tuvan.IdTuVan = Guid.NewGuid().ToString();
                tuvan.Email = input.Email;
                tuvan.Ten = input.Ten;
                tuvan.LoiNhan = input.LoiNhan;
                tuvan.NgayGioNhan = DateTime.Now;
                var tb = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "email");
                tb.Tttb++;
                _context.TuVans.Add(tuvan);
                _context.Update(tb);
                _context.SaveChanges();
                return tuvan;
            }
            return null;
        }
        [HttpDelete("xoa-tu-van/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult XoaTuVan(string id)
        {
            if (ModelState.IsValid)
            {
                var tuvan = _context.TuVans.FirstOrDefault(c => c.IdTuVan == id);
                if (tuvan != null)
                {
                    _context.TuVans.Remove(tuvan);
                    _context.SaveChanges();
                    return Ok();
                }
                else return BadRequest();
            }
            return BadRequest();
        }
        [HttpGet("thong-bao-tu-van")]
        [Authorize(Roles = "Admin")]
        public IActionResult ThongBaoTuVan()
        {
            var tb = _context.ThongBaos.FirstOrDefault(c => c.TenThongBao == "email");
            return Ok(tb.Tttb);
        }
    }
}
