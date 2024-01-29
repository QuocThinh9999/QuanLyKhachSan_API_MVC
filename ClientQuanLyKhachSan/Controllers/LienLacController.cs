using System.Security.Policy;
using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Controllers
{
    public class LienLacController : Controller
    {
        private readonly HttpClient _httpClient;

        public LienLacController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("lien-lac")]
        public async Task<IActionResult> LienLac(string email)
        {
            var items = await GetLienLac(email);
            return View(items);
        }
        private async Task<ModelViewUser> GetLienLac(string email)
        {
            string url1 = "http://localhost:5006/api/ThongTinWeb/danh-sach-thong-tin-web";

            ModelViewUser modelViewUser = new ModelViewUser();
            
                var res = await _httpClient.GetAsync(url1);
                if (res.IsSuccessStatusCode)
                {

                    var lisitems = await res.Content.ReadAsAsync<List<ModelViewUser.ThongTinWeb>>();
                    modelViewUser.ThongTinWebs = lisitems;
                }
            
            modelViewUser.email = email;
            modelViewUser.TrangThaiTuVan = 0;
            return modelViewUser;
        }
        [Route("lien-lac")]
        [HttpPost]
        public async Task<IActionResult> LienLac(string Email, string LoiNhan, string Ten)
        {
            await pvLienLac(Email, LoiNhan, Ten);
            var items = await GetLienLac(Email);
            items.TrangThaiTuVan = 1;
            return View(items);
        }
        private async Task pvLienLac(string Email, string LoiNhan, string Ten)
        {
            ModelViewUser modelViewUser=new ModelViewUser();
            string url = "http://localhost:5006/api/TuVan/them-tu-van";
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(Email), "Email");
            data.Add(new StringContent(LoiNhan), "LoiNhan");
            data.Add(new StringContent(Ten), "Ten");
            await _httpClient.PostAsync(url,data);
            
        }
    }
}
