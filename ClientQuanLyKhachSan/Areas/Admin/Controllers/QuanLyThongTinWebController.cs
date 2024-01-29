using ClientQuanLyKhachSan.Areas.Admin.Models;
using ClientQuanLyKhachSan.Common;
using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("quan-ly-thong-tin-web")]
    public class QuanLyThongTinWebController : Controller
    {
        private readonly HttpClient _httpClient;

        public QuanLyThongTinWebController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("danh-sach-thong-tin-web")]
        public async Task<IActionResult> DanhSachThongTinWeb()
        {
            var items = await GetThongTinWeb();
            return View(items);
        }
        private async Task<ModelViewAdmin> GetThongTinWeb()
        {
            string url = "http://localhost:5006/api/ThongTinWeb/danh-sach-thong-tin-web";
            ModelViewAdmin thongtinwebs = null;
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {
                thongtinwebs = new ModelViewAdmin();
                var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.ThongTinWeb>>();
                thongtinwebs.ThongTinWebs = lisitems;
            }

            return thongtinwebs;
        }
        [Route("danh-sach-thong-tin-web")]
        [HttpPost]
        public async Task<IActionResult> DanhSachThongTinWeb(string? ten)
        {
            var items = await GetThongTinWeb();
            
            items.ThongTinWebs= items.ThongTinWebs.Where(c => c.LoaiThongTin.Contains((ten ?? "").ToLower())).ToList();
            return View(items);
        }
        [Route("cap-nhat-thong-tin-web")]
        public async Task<IActionResult> CapNhatThongTinWeb(string id)
        {
            var items = await GetThongTinWeb();
            var listitem = items.ThongTinWebs.FirstOrDefault(c => c.LoaiThongTin == id);
            return View(listitem);
        }
        [Route("cap-nhat-thong-tin-web")]
        [HttpPost]
        public async Task<IActionResult> CapNhatThongTinWeb(ModelViewAdmin.ThongTinWeb input)
        {
            var items = await PostCapNhatThongTinWeb(input);
            return RedirectToAction("DanhSachThongTinWeb", "QuanLyThongTinWeb", new { Areas = "Admin" });
        }
        private async Task<ModelViewAdmin> PostCapNhatThongTinWeb(ModelViewAdmin.ThongTinWeb input)
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            modelViewAdmin.ThongTinWebs = new List<ModelViewAdmin.ThongTinWeb>();
            string url = "http://localhost:5006/api/ThongTinWeb/cap-nhat-thong-tin-web";
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(input.LoaiThongTin), "LoaiThongTin");
            data.Add(new StringContent(input.MoTa), "MoTa");
            if (input.Images != null)
            {
                input.UrlImages = UploadFiles.SaveImage(input.Images);
            }
            if (input.UrlImages != null)
            {
                data.Add(new StringContent(input.UrlImages), "UrlImages");
            }
            var res = await _httpClient.PutAsync(url, data);
            if(res.IsSuccessStatusCode)
            {
                modelViewAdmin.ThongTinWebs.Add(input);
                return modelViewAdmin;
            }
            return null;
        }

    }
}
