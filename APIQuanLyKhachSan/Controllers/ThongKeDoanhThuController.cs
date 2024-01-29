using APIQuanLyKhachSan.Models;
using APIQuanLyKhachSan.Models.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ThongKeDoanhThuController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _dbContext;
        public ThongKeDoanhThuController(DbQuanLyKhachSanContext context)
        {
            _dbContext = context;
        }
        [HttpPost("thong-ke-doanh-thu")]
        public IActionResult ThongKeDoanhThu(int nam)
        {
            var items = GetThongKeDoanhThu(nam);
            return Ok(items);
        }
        private List<ThongKeDoanhThu.DoanhThu> GetThongKeDoanhThu(int nam)
        {
            var tkdts = new List<ThongKeDoanhThu.DoanhThu>();
            var hds = _dbContext.HoaDons.Where(c => c.TrangThai == "Thành công" && c.GioCheckin.Year == nam).ToList();
            for (int i = 1; i < 13; i++)
            {
                var tkdt = new ThongKeDoanhThu.DoanhThu();
                tkdt.Nam = nam;
                tkdt.Thang = i;
                tkdt.DoanhThuThang = 0;
                tkdts.Add(tkdt);
            }
            foreach (var hd in hds)
            {
                var tkdt = tkdts.FirstOrDefault(c => c.Thang == hd.GioCheckin.Month);
                tkdt.DoanhThuThang += hd.TongTien;
            }
            return tkdts;
        }
        [HttpPost("thong-ke-loai-phong")]
        public IActionResult ThongKeLoaiPhong([FromForm] int thang, [FromForm] int nam)
        {
            var items = GetThongKeLoaiPhong(thang, nam);
            return Ok(items);
        }
        private List<ThongKeDoanhThu.ThongKePhong> GetThongKeLoaiPhong(int thang, int nam)
        {
            var items = new List<ThongKeDoanhThu.ThongKePhong>();
            var hds = _dbContext.HoaDons.Where(c => c.TrangThai == "Thành công" && c.GioCheckin.Month == thang && c.GioCheckin.Year == nam).ToList();
            foreach (var hd in hds)
            {
                if (items.Count == 0)
                {
                    var item = new ThongKeDoanhThu.ThongKePhong();
                    item.TenPhong = hd.TenPhong;
                    item.SoLuong = 1;
                    items.Add(item);
                }
                else
                {
                    int i = 0;
                    foreach (var item in items)
                    {
                        if (item.TenPhong == hd.TenPhong)
                        {
                            item.SoLuong++;
                            i = 1;
                            break;
                        }
                    }
                    if (i == 0)
                    {
                        var item = new ThongKeDoanhThu.ThongKePhong();
                        item.TenPhong = hd.TenPhong;
                        item.SoLuong = 1;
                        items.Add(item);
                    }
                }
            }

            return items;
        }
    }
}
