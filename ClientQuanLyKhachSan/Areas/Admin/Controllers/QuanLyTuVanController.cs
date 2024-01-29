using ClientQuanLyKhachSan.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("quan-ly-tu-van")]
    public class QuanLyTuVanController : Controller
    {
        private readonly HttpClient _httpClient;

        public QuanLyTuVanController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("danh-sach-tu-van")]
        public async Task<IActionResult> DanhSachTuVan()
        {
            var item = await pvDanhSachTuVan();
            return View(item);
        }
        [Route("danh-sach-tu-van")]
        [HttpPost]
        public async Task<IActionResult> DanhSachTuVan(string ten)
        {
            var items = await pvDanhSachTuVan();
            items.tuVan = items.tuVan.Where(c => c.Ten.Contains((ten ?? "").ToLower()) || c.Email.Contains((ten ?? "").ToLower())).ToList();
            return View(items);
        }
        private async Task<ModelViewAdmin> pvDanhSachTuVan()
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            string url2 = "http://localhost:5006/api/TuVan/thong-bao-tu-van";
            var res2 = await _httpClient.GetAsync(url2);
            if (res2.IsSuccessStatusCode)
            {
                var lisitems = await res2.Content.ReadAsAsync<int>();
                modelViewAdmin.ThongBaoTuVan = lisitems;
            }
            string url = "http://localhost:5006/api/TuVan/danh-sach-tu-van";
            
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.TuVan>>();
                modelViewAdmin.tuVan = lisitems;
            }
            
            return modelViewAdmin;
        }
        [Route("xoa-tu-van")]
        public async Task<IActionResult> XoaTuVan(string id)
        {
            pvXoaTuVan(id);
            return RedirectToAction("DanhSachTuVan", "QuanLyTuVan", new { Areas = "Admin" });
        }
        private async Task pvXoaTuVan(string id)
        {
            string url = "http://localhost:5006/api/TuVan/xoa-tu-van/" + id;
            var res = await _httpClient.DeleteAsync(url);
        }
    }
}
