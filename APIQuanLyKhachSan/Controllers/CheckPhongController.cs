using APIQuanLyKhachSan.Models;
using APIQuanLyKhachSan.Models.ModelView;
using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckPhongController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _context;
        public CheckPhongController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }
        [HttpPost("danh-sach-check-phong")]
        public IActionResult Danhsachcheckphong([FromForm] string dscp)
        {
            var item = pvDanhsachcheckphong(dscp);
            return Ok(item);
        }
        private List<Phong> pvDanhsachcheckphong([FromForm] string dscp)
        {
            var input = System.Text.Json.JsonSerializer.Deserialize<CheckDanhSachPhong>(dscp);
            var danhSachPhong = _context.Phongs
        .Join(_context.ChiTietPhongs,
              phong => phong.Id,
              ctp => ctp.IdPhong,
              (phong, ctp) => new { Phong = phong, ChiTietPhong = ctp })
        .Where(x => x.Phong.GiaPhong < input.GiaPhongMax &&
                    x.Phong.GiaPhong > input.GiaPhongMin &&
                    x.ChiTietPhong.SoNguoiLon >= input.SoNguoiLon &&
                    x.ChiTietPhong.SoTreEm >= input.SoTreEm)
        .Select(x => x.Phong)
        .ToList();
            List<Phong> item = new List<Phong>();
            foreach (var p in danhSachPhong)
            {
                var hoadons = _context.HoaDons.Where(c => c.IdPhong == p.Id && c.TrangThai == "Chờ nhận phòng").ToList();
                int i = 0;
                foreach (var cp in hoadons)
                {
                    if ((cp.GioCheckin <= input.GioCheckin && input.GioCheckin < cp.GioCheckout)
                        || (cp.GioCheckin < input.GioCheckout && input.GioCheckout <= cp.GioCheckout)
                        || (input.GioCheckin < cp.GioCheckin && cp.GioCheckout < input.GioCheckout))
                    {
                        i++;
                    }
                }
                if (p.SoLuong > i)
                {
                    var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(p.UrlImage);
                    p.UrlImage = ListUrl[0].UrlImage;
                    item.Add(p);
                }
            }
            return item;
        }
        [HttpGet("check-phong/{id}")]
        public IActionResult CheckPhong(string id)
        {
            var checkphong = pvCheckPhong(id);
            return Ok(checkphong);
        }
        private List<CheckPhong.NgayKhongKhaDung> pvCheckPhong(string id)
        {
            var phong = _context.Phongs.FirstOrDefault(c => c.Id == id);
            var hoadons = _context.HoaDons.Where(c => c.IdPhong == phong.Id && c.TrangThai == "Chờ nhận phòng").ToList();
            var checkphong = new List<CheckPhong.NgayKhongKhaDung>();
            var checkphong2 = new List<CheckPhong.NgayKhongKhaDung>();
            if (hoadons.Count == 0)
            {
                return checkphong;
            }
            if (phong.SoLuong == 1)
            {
                foreach (var item in hoadons)
                {
                    var cp = new Models.ModelView.CheckPhong.NgayKhongKhaDung();
                    cp.GioCheckin = item.GioCheckin;
                    cp.GioCheckout = item.GioCheckout;
                    checkphong.Add(cp);
                }
                return checkphong;
            }
            if (hoadons.Count == 1)
            {
                if (phong.SoLuong > 1) return checkphong;
                var nkkd = new CheckPhong.NgayKhongKhaDung();
                nkkd.GioCheckin = hoadons[0].GioCheckin;
                nkkd.GioCheckout = hoadons[0].GioCheckout;
                checkphong.Add(nkkd);
                return checkphong;
            }
            var checktam = new CheckPhong.NgayKhongKhaDung();

            var check = CheckNgay(hoadons, phong.SoLuong, 0, 0, checktam, checkphong2);
            foreach (var item in check)
            {
                checkphong.Add(item);
            }
            return checkphong;
        }
        private List<CheckPhong.NgayKhongKhaDung> CheckNgay(List<HoaDon> hoadons, int? soPhong, int start, int check, CheckPhong.NgayKhongKhaDung checkTam, List<CheckPhong.NgayKhongKhaDung> checkPhongs)
        {

            if (check == soPhong)
            {
                checkPhongs.Add(checkTam);
                checkTam = new CheckPhong.NgayKhongKhaDung();
                check = 0;
            }
            for (int i = start; i < hoadons.Count; i++)
            {
                if (check == 0)
                {
                    checkTam.GioCheckin = hoadons[i].GioCheckin;
                    checkTam.GioCheckout = hoadons[i].GioCheckout;
                    return CheckNgay(hoadons, soPhong, i + 1, check + 1, checkTam, checkPhongs);
                }
                if (hoadons[i].GioCheckin <= checkTam.GioCheckin && checkTam.GioCheckout <= hoadons[i].GioCheckout)
                {
                    return CheckNgay(hoadons, soPhong, i + 1, check + 1, checkTam, checkPhongs);
                }
                if (hoadons[i].GioCheckin <= checkTam.GioCheckin && checkTam.GioCheckin < hoadons[i].GioCheckout && hoadons[i].GioCheckout <= checkTam.GioCheckout)
                {
                    checkTam.GioCheckout = hoadons[i].GioCheckout;
                    return CheckNgay(hoadons, soPhong, i + 1, check + 1, checkTam, checkPhongs);
                }
                if (checkTam.GioCheckin <= hoadons[i].GioCheckin && hoadons[i].GioCheckin < checkTam.GioCheckout && hoadons[i].GioCheckout >= checkTam.GioCheckout)
                {
                    checkTam.GioCheckin = hoadons[i].GioCheckin;
                    return CheckNgay(hoadons, soPhong, i + 1, check + 1, checkTam, checkPhongs);
                }

            }
            return checkPhongs;
        }

    }
}
