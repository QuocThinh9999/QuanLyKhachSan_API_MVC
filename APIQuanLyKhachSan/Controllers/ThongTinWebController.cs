using APIQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQuanLyKhachSan.Models.ThemDuLieu;

namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongTinWebController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _context;
        public ThongTinWebController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }

        [HttpGet("danh-sach-thong-tin-Web")]
        public IActionResult Danhsachthongtinweb()
        {
            var tt = _context.ThongTinWebs.ToList();
            return Ok(tt);
        }

        [HttpPut("cap-nhat-thong-tin-web")]
        [Authorize(Roles = "Admin")]
        public IActionResult CapNhatThongTinWeb([FromForm] ThongTinWeb input)
        {
            var item = _context.ThongTinWebs.FirstOrDefault(c => c.LoaiThongTin == input.LoaiThongTin);

            if (item != null)
            {
                item.MoTa = input.MoTa;
                if (input.UrlImages != null)
                {

                    item.UrlImages = input.UrlImages;
                }
                _context.Update(item);
                _context.SaveChanges();
                return Ok();
            }

            else return BadRequest();
        }
    }
}
