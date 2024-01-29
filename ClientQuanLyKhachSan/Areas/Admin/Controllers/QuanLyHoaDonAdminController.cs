using ClientQuanLyKhachSan.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("quan-ly-hoa-don")]
    public class QuanLyHoaDonAdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public QuanLyHoaDonAdminController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("danh-sach-hoa-don")]
        public async Task<IActionResult> DanhSachHoaDon()
        {
            var item = await GetDanhSachHoaDon();
            return View(item);
        }
        [Route("danh-sach-hoa-don")]
        [HttpPost]
        public async Task<IActionResult> DanhSachHoaDon(string ten)
        {
            var items = await GetDanhSachHoaDon();
            items.HoaDonAdmins = items.HoaDonAdmins.Where(c => c.TenNguoiDung.Contains((ten ?? "").ToLower())|| c.email.Contains((ten ?? "").ToLower())).ToList();
            return View(items);
        }
        private async Task<ModelViewAdmin> GetDanhSachHoaDon()
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            string url2 = "http://localhost:5006/api/HoaDon/thong-bao-hoa-don";
            var res2 = await _httpClient.GetAsync(url2);
            if (res2.IsSuccessStatusCode)
            {
                var lisitems = await res2.Content.ReadAsAsync<int>();
                modelViewAdmin.ThongBaoHoaDon = lisitems;
            }
            string url = "http://localhost:5006/api/HoaDon/danh-sach-hoa-don";
           
            modelViewAdmin.HoaDonAdmins = new List<ModelViewAdmin.HoaDonAdmin>();
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                
                var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.HoaDonAdmin>>();
                modelViewAdmin.HoaDonAdmins = lisitems;
            }

            return modelViewAdmin;
        }
        [Route("cap-nhat-hoa-don")]
        public async Task<IActionResult> CapNhatHoaDon(string id)
        {
            var item = await GetCapNhatDanhSachHoaDon(id);
            return View(item);
        }
        private async Task<ModelViewAdmin> GetCapNhatDanhSachHoaDon(string id)
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            var items = await GetDanhSachHoaDon();
            var item = items.HoaDonAdmins.FirstOrDefault(c => c.IdHoaDon == id);
            modelViewAdmin.HoaDonAdmins = new List<ModelViewAdmin.HoaDonAdmin> { item };

            return modelViewAdmin;
        }
        [Route("cap-nhat-hoa-don")]
        [HttpPost]
        public async Task<IActionResult> ThayDoiTrangThaiHoaDon(string IdHoaDon, string trangthai)
        {
            PostCapNhatTrangThaiHoaDon(IdHoaDon, trangthai);
            return RedirectToAction("DanhSachHoaDon", "QuanLyHoaDonAdmin", new { Areas = "Admin" });
        }
        private async Task<ModelViewAdmin> PostCapNhatTrangThaiHoaDon(string id, string trangthai)
        {
            string url = "http://localhost:5006/api/HoaDon/doi-trang-thai-hoa-don/"+id;
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            modelViewAdmin.HoaDonAdmins = new List<ModelViewAdmin.HoaDonAdmin>();
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(id), "id");
            data.Add(new StringContent(trangthai), "trangthai");

            var res = await _httpClient.PatchAsync(url, data);
            //if (res.IsSuccessStatusCode)
            //{
            //    modelViewAdmin = new ModelViewAdmin();
            //    var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.HoaDonAdmin>>();
            //    modelViewAdmin.HoaDonAdmins = lisitems;
            //}

            return modelViewAdmin;
        }
    }
}
