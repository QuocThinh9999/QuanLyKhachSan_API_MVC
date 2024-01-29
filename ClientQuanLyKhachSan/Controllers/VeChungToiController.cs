using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Controllers
{
    public class VeChungToiController : Controller
    {
        private readonly HttpClient _httpClient;

        public VeChungToiController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("ve-chung-toi")]
        public async Task<IActionResult> DanhSachVeChungToi()
        {
            var items = await GetDanhSachVeChungToi();
            return View(items);
        }
        private async Task<ModelViewUser> GetDanhSachVeChungToi()
        {
            string url1 = "http://localhost:5006/api/ThongTinWeb/danh-sach-thong-tin-web";

            ModelViewUser modelViewUser = new ModelViewUser();
            
                var res = await _httpClient.GetAsync(url1);
                if (res.IsSuccessStatusCode)
                {

                    var lisitems = await res.Content.ReadAsAsync<List<ModelViewUser.ThongTinWeb>>();
                    modelViewUser.ThongTinWebs = lisitems;
                }
            
            string url = "http://localhost:5006/api/QuanLyPhong/danh-sach-phong";
            
                var res2 = await _httpClient.GetAsync(url);
                if (res2.IsSuccessStatusCode)
                {

                    var lisitems = await res2.Content.ReadAsAsync<List<ModelViewUser.PhongTrangChu>>();
                    modelViewUser.PhongTrangChus = lisitems;
                }
            

            return modelViewUser;
        }
    }
}
