using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Controllers
{
    public class BaiVietController : Controller
    {
        private readonly HttpClient _httpClient;

        public BaiVietController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("danh-sach-bai-viet")]
        public async Task<IActionResult> DanhSachBaiViet()
        {
            var items = await GetDanhSachBaiViet();
            return View(items);
        }
        private async Task<ModelViewUser> GetDanhSachBaiViet()
        {
            string url = "http://localhost:5006/api/QuanLyBaiViet/danh-sach-bai-viet";
            ModelViewUser modelViewUser = new ModelViewUser();         
            
                var res = await _httpClient.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {

                    var lisitems = await res.Content.ReadAsAsync<List<ModelViewUser.BlogTrangChu>>();
                    modelViewUser.BlogTrangChus = lisitems;
                }
            

            return modelViewUser;
        }
        [Route("chi-tiet-bai-viet")]
        public async Task<IActionResult> ChiTietBaiViet(string id)
        {
            var items = await GetChiTietBaiViet(id);
            return View(items);
        }
        private async Task<ModelViewUser> GetChiTietBaiViet(string id)
        {
            string url1 = "http://localhost:5006/api/QuanLyBaiViet/danh-sach-bai-viet";
            ModelViewUser modelViewUser = new ModelViewUser();
            
                var res = await _httpClient.GetAsync(url1);
                if (res.IsSuccessStatusCode)
                {

                    var lisitems = await res.Content.ReadAsAsync<List<ModelViewUser.BlogTrangChu>>();
                    modelViewUser.BlogTrangChus = lisitems;
                }
            
            string url2 = "http://localhost:5006/api/QuanLyBaiViet/chi-tiet-bai-viet/"+id;
            
            
                var res2 = await _httpClient.GetAsync(url2);
                if (res2.IsSuccessStatusCode)
                {

                    var lisitems = await res2.Content.ReadAsAsync<ModelViewUser.ChiTietBlog>();
                    modelViewUser.ChiTietBlogs = lisitems;
                }
            
            return modelViewUser;
        }
    }
}
