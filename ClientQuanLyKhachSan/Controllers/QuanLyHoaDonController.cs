using System.IdentityModel.Tokens.Jwt;
using ClientQuanLyKhachSan.Areas.Admin.Views.TaiKhoan;
using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using static ClientQuanLyKhachSan.Models.ModelViewUser;

namespace ClientQuanLyKhachSan.Controllers
{
    public class QuanLyHoaDonController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuanLyHoaDonController(IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient.CreateClient("Client");
            _httpContextAccessor = httpContextAccessor;
        }
        [Route("lich-su-dat-phong")]
        public async Task<IActionResult> LichSuDatPhong()
        {
            var items = await GetLichSuDatPhong();
            return View(items);
        }
        [Route("lich-su-chi-tiet")]
        public async Task<IActionResult> LichSuChiTiet(string id)
        {
            var items = await GetLichSuDatPhong();
            List<ModelViewUser.LichSuDatPhong> lsdp = items.lichSuDatPhongs;
            var nguoidung = await GetCapNhatThongTinCaNhan();
            ModelViewUser.NguoiDung nd = nguoidung.nguoiDung;
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser = await GetLichSuChiTiet(nd, lsdp,id);
            return View(modelViewUser);
        }
        private async Task<ModelViewUser> GetLichSuChiTiet(ModelViewUser.NguoiDung? nguoidung, List<ModelViewUser.LichSuDatPhong> items, string id)
        {
            
            var item = items.FirstOrDefault(c => c.IdHoaDon == id);
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser.lichSuChiTiet = new ModelViewUser.LichSuChiTiet();
            modelViewUser.lichSuChiTiet.IdHoaDon = item.IdHoaDon;
            modelViewUser.lichSuChiTiet.TenPhong = item.TenPhong;
            modelViewUser.lichSuChiTiet.IdPhong = item.IdPhong;
            modelViewUser.lichSuChiTiet.SoNguoiLon = item.SoNguoiLon;
            modelViewUser.lichSuChiTiet.SoTreEm = item.SoTreEm;
            modelViewUser.lichSuChiTiet.GioCheckin = item.GioCheckin;
            modelViewUser.lichSuChiTiet.GioCheckout = item.GioCheckout;
            modelViewUser.lichSuChiTiet.TongNgay = (int)(item.GioCheckout - item.GioCheckin).TotalDays;
            modelViewUser.lichSuChiTiet.TongTien = item.TongTien;
            modelViewUser.lichSuChiTiet.TrangThai = item.TrangThai;
            modelViewUser.lichSuChiTiet.YeuCau = item.YeuCau;
            modelViewUser.lichSuChiTiet.UrlImages = item.UrlImages;
            
            modelViewUser.lichSuChiTiet.TenNguoiDung = nguoidung.Ten;
            modelViewUser.lichSuChiTiet.EmailNguoiDung = nguoidung.Email;
            modelViewUser.lichSuChiTiet.SDTNguoiDung = nguoidung.SoDienThoai;
            return modelViewUser;
        }
        [HttpPost("mau-hoa-don")]
        public async Task<IActionResult> MauHoaDon(ModelViewUser.LichSuChiTiet input)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Cookies["Token"];
            if (token == null)
            {
                return Redirect("/dang-nhap?Areas=Admin");
            }
            var modelViewUser= await GetMauHoaDon(input);
            return View(modelViewUser);

        }
        private async Task<ModelViewUser> GetMauHoaDon(ModelViewUser.LichSuChiTiet input)
        {
            
            var nguoidung = await GetCapNhatThongTinCaNhan();
            var phong = await GetChiTietPhong(input.IdPhong);
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser.lichSuChiTiet = new ModelViewUser.LichSuChiTiet();
            modelViewUser.lichSuChiTiet.IdPhong = phong.ChiTietTrangPhongs.id;
            modelViewUser.lichSuChiTiet.SoNguoiLon = phong.ChiTietTrangPhongs.SoNguoiLon;
            modelViewUser.lichSuChiTiet.SoTreEm = phong.ChiTietTrangPhongs.SoTreEm;
            modelViewUser.lichSuChiTiet.GioCheckin = input.GioCheckin;
            modelViewUser.lichSuChiTiet.GioCheckout = input.GioCheckout;
            modelViewUser.lichSuChiTiet.TongNgay = (int)(input.GioCheckout - input.GioCheckin).TotalDays;
            modelViewUser.lichSuChiTiet.TongTien = (modelViewUser.lichSuChiTiet.TongNgay * phong.ChiTietTrangPhongs.GiaSauGiam) * (1 + 0.08m);
            modelViewUser.lichSuChiTiet.UrlImages = phong.ChiTietTrangPhongs.UrlImages[0];
            modelViewUser.lichSuChiTiet.TenNguoiDung = nguoidung.nguoiDung.Ten;
            modelViewUser.lichSuChiTiet.EmailNguoiDung = nguoidung.nguoiDung.Email;
            modelViewUser.lichSuChiTiet.SDTNguoiDung = nguoidung.nguoiDung.SoDienThoai;
            return modelViewUser;
        }
        [HttpPost("tao-hoa-don")]
        public async Task<IActionResult> TaoHoaDon(ModelViewUser.TaoHoaDon input)
        {
            await PostTaoHoaDon(input);
            return RedirectToAction("LichSuDatPhong", "QuanLyHoaDon");
        }
        private async Task PostTaoHoaDon(ModelViewUser.TaoHoaDon input)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Cookies["Token"];
            string id = GetUserIdFromToken(token);
            string url = "http://localhost:5006/api/HoaDon/tao-hoa-don";
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser.taoHoaDon= input;
            modelViewUser.taoHoaDon.IdNguoiDung= id;
            var data = new MultipartFormDataContent();
            var taohoadon = System.Text.Json.JsonSerializer.Serialize(modelViewUser.taoHoaDon);
            data.Add(new StringContent(taohoadon), "taohoadon");
            var res = await _httpClient.PostAsync(url,data);
        }
        [Route("huy-hoa-don")]
        public async Task<IActionResult> HuyDatPhong(string id)
        {
            var item = await PostHuyHoaDon(id);
            var items = await GetLichSuDatPhong();
            return RedirectToAction("LichSuDatPhong", "QuanLyHoaDon");
            
        }
        private async Task<ModelViewUser> PostHuyHoaDon(string id)
        {
            string url = "http://localhost:5006/api/HoaDon/doi-trang-thai-hoa-don/"+id;
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(id), "id");
            data.Add(new StringContent("Đã hủy"), "trangthai");
            var res = await _httpClient.PatchAsync(url,data);
            ModelViewUser modelViewUser = new ModelViewUser();
            return modelViewUser;
        }
        private async Task<ModelViewUser> GetLichSuDatPhong()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Cookies["Token"];
            string id = GetUserIdFromToken(token);
            string url = "http://localhost:5006/api/HoaDon/lich-su-dat-phong/" + id;
            ModelViewUser modelViewUser = new ModelViewUser();
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<List<ModelViewUser.LichSuDatPhong>>();
                modelViewUser.lichSuDatPhongs = lisitems;
            }


            return modelViewUser;
        }
        private string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            // Trích xuất id từ payload
            var id = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid")?.Value;
            return id;
        }
        private async Task<ModelViewUser> GetCapNhatThongTinCaNhan()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Cookies["Token"];
            string id = GetUserIdFromToken(token);
            string url = "http://localhost:5006/api/QuanLyThongTinCaNhan/xem-thong-tin-ca-nhan/" + id;
            ModelViewUser modelViewUser = new ModelViewUser();
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<ModelViewUser.NguoiDung>();
                modelViewUser.nguoiDung = lisitems;
            }


            return modelViewUser;
        }
        private async Task<ModelViewUser> GetChiTietPhong(string id)
        {
            string url1 = "http://localhost:5006/api/QuanLyPhong/chi-tiet-phong/" + id;

            ModelViewUser modelViewUser = new ModelViewUser();
            var res = await _httpClient.GetAsync(url1);

            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<ModelViewUser.ChiTietTrangPhong>();
                modelViewUser.ChiTietTrangPhongs = lisitems;
            }

            string url2 = "http://localhost:5006/api/QuanLyPhong/danh-sach-phong";

            var res2 = await _httpClient.GetAsync(url2);
            if (res2.IsSuccessStatusCode)
            {

                var lisitems = await res2.Content.ReadAsAsync<List<ModelViewUser.PhongTrangChu>>();
                modelViewUser.PhongTrangChus = lisitems;
            }
            return modelViewUser;
        }
    }
}
