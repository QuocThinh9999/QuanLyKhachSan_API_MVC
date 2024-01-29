using System.Text.Json;
using APIQuanLyKhachSan.Models;
using APIQuanLyKhachSan.Models.ModelView;
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
    public class QuanLyBaiVietController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _context;
        public QuanLyBaiVietController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet("danh-sach-bai-viet")]
        public IActionResult DanhSachBaiViet()
        {
            List<Blog> blogs = new List<Blog>();
            var items = _context.Blogs.ToList();
            foreach (var b in items)
            {
                Blog blog = new Blog();
                blog.Tieude = b.Tieude;
                blog.IdBlog = b.IdBlog;
                blog.MoTa = b.MoTa;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(b.UrlImage);
                blog.UrlImage = ListUrl[0].UrlImage;
                blogs.Add(blog);
            }
            blogs = blogs.OrderBy(c => Guid.NewGuid()).ToList();
            return Ok(blogs);
        }
        [AllowAnonymous]
        [HttpGet("chi-tiet-bai-viet/{id}")]
        public IActionResult ChiTietBaiViet(string id)
        {
            var blog = new ChiTietBlog();
            var item = _context.Blogs.FirstOrDefault(c=>c.IdBlog == id);
            blog.Tieude = item.Tieude;
            blog.IdBlog = item.IdBlog;
            blog.MoTa = item.MoTa;
            var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(item.UrlImage);

            blog.UrlImages = new List<string>();
            foreach (var url in ListUrl)
            {
                blog.UrlImages.Add(url.UrlImage);
            }
            return Ok(blog);
        }
        [HttpDelete("xoa-bai-viet/{id}")]
        public IActionResult XoaBaiViet(string id)
        {
            var item = _context.Blogs.FirstOrDefault(c => c.IdBlog == id);
            _context.Remove(item);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost("them-bai-viet")]
        public IActionResult ThemBaiViet([FromForm]string tbv)
        {
            var input = System.Text.Json.JsonSerializer.Deserialize<ThemBaiViet>(tbv);
            if (ModelState.IsValid)
            {
                string id = Guid.NewGuid().ToString();
                Blog baiviet = new Blog
                {
                    IdBlog = id,
                    Tieude=input.Tieude,
                    MoTa=input.MoTa,
                };
                List<OutputImage> listimage = new List<OutputImage>();
                if (input.UrlImages != null)
                {
                    foreach (var img in input.UrlImages)//
                    {
                        OutputImage img2 = new OutputImage();
                        img2.Position = 1;
                        img2.UrlImage = img;
                        listimage.Add(img2);
                    }
                }
                else
                {
                    OutputImage img2 = new OutputImage();
                    img2.Position = 1;
                    img2.UrlImage = "\\images\\no_image.jpg";
                    listimage.Add(img2);
                }
                baiviet.UrlImage = JsonSerializer.Serialize(listimage);
                _context.Blogs.Add(baiviet);
                _context.SaveChanges();
                return Ok();
            }
            else return BadRequest();
        }
        [HttpPut("cap-nhat-bai-viet/{id}")]
        public IActionResult CapNhatPhong([FromForm] string id, [FromForm] string cnbv)
        {
            if (ModelState.IsValid)
            {
                var input = System.Text.Json.JsonSerializer.Deserialize<ThemBaiViet>(cnbv);
                var item = _context.Blogs.FirstOrDefault(x => x.IdBlog == id);
                if (item == null)
                {
                    return NotFound();
                }
                else
                {
                    item.Tieude = input.Tieude;
                    item.MoTa=input.MoTa;
                }

                if (input.UrlImages != null)
                {
                    List<OutputImage> listimage = new List<OutputImage>();
                    foreach (var img in input.UrlImages)
                    {
                        OutputImage img2 = new OutputImage();
                        img2.Position = 1;
                        img2.UrlImage = img;
                        listimage.Add(img2);

                    }
                    item.UrlImage = JsonSerializer.Serialize(listimage);
                }

                _context.Update(item);
                _context.SaveChanges();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
