using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ClientQuanLyKhachSan.Areas.Admin.Models;
using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Controllers
{
    public class TrangChuController : Controller
    {
        private readonly HttpClient _httpClient;

        public TrangChuController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }

        public async Task<IActionResult> TrangChu()
        {
            var items = await GetTrangChu();
            return View(items);
        }  
        private async Task<ModelViewUser> GetTrangChu()
        {
           

            ModelViewUser modelViewUser = new ModelViewUser();
            

            string url2 = "http://localhost:5006/api/QuanLyPhong/danh-sach-phong";
            var res2 = await _httpClient.GetAsync(url2);
            if (res2.IsSuccessStatusCode)
            {

                var lisitems = await res2.Content.ReadAsAsync<List<ModelViewUser.PhongTrangChu>>();
                modelViewUser.PhongTrangChus = lisitems;
            }

            string url3 = "http://localhost:5006/api/QuanLyBaiViet/danh-sach-bai-viet";

            var res3 = await _httpClient.GetAsync(url3);
            if (res3.IsSuccessStatusCode)
            {

                var lisitems = await res3.Content.ReadAsAsync<List<ModelViewUser.BlogTrangChu>>();
                modelViewUser.BlogTrangChus = lisitems;
            }

            return modelViewUser;
        }
        
    }
}