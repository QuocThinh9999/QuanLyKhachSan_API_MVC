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
    [Authorize(Roles = "Admin")]
    public class GiamGiaController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _context;
        public GiamGiaController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [HttpGet("danh-sach-giam-gia")]
        public IActionResult DanhSachGiamGia()
        {
            var items = _context.Phongs.ToList();
            return Ok(items);
        }
        [HttpPatch("quan-ly-giam-gia/{id}")]
        public IActionResult QuanLyGiamGia([FromForm] string id,[FromForm] string gg)
        {
            if (ModelState.IsValid)
            {
                var input = System.Text.Json.JsonSerializer.Deserialize<GiamGia>(gg);
                var item = _context.Phongs.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                else
                {
                    item.GiaPhong = input.GiaPhong;
                    item.GiaSauGiam = input.GiaSauGiam;
                    _context.Update(item);
                    _context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
