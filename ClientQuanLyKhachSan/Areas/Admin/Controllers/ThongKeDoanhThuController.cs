using ClientQuanLyKhachSan.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("thong-ke-doanh-thu")]
    public class ThongKeDoanhThuController : Controller
    {
        private readonly HttpClient _httpClient;

        public ThongKeDoanhThuController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        public async Task<IActionResult> ThongKeDoanhThu()
        {
            var item = await GetThongKeDoanhThu();
            return View(item);
        }
        private async Task<ModelViewAdmin> GetThongKeDoanhThu()
        {
            string url = "http://localhost:5006/api/ThongKeDoanhThu/thong-ke-doanh-thu";
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            modelViewAdmin.doanhThus = new List<ModelViewAdmin.DoanhThu>();
            //var data = new MultipartFormDataContent();
            //data.Add(new StringContent(id), "id");
            //data.Add(new StringContent(trangthai), "trangthai");

            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                modelViewAdmin = new ModelViewAdmin();
                var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.DoanhThu>>();
                modelViewAdmin.doanhThus = lisitems;
            }

            return modelViewAdmin;
        }
    }
}
