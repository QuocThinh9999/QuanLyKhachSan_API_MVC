using ClientQuanLyKhachSan.Areas.Admin.Models;
using ClientQuanLyKhachSan.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("quan-ly-giam-gia")]
    public class GiamGiaController : Controller
    {
        private readonly HttpClient _httpClient;

        public GiamGiaController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("danh-sach-giam-gia")]
        public async Task<IActionResult> DanhSachGiamGia()
        {
            var item = await pvDanhSachGiamGia();
            return View(item);
        }
        [Route("danh-sach-giam-gia")]
        [HttpPost]
        public async Task<IActionResult> DanhSachGiamGia(string ten)
        {
            var items = await pvDanhSachGiamGia();
            items.PhongTrangChus = items.PhongTrangChus.Where(c => c.TenPhong.Contains((ten ?? "").ToLower())).ToList();
            return View(items);
        }
        private async Task<ModelViewAdmin> pvDanhSachGiamGia()
        {
            string url = "http://localhost:5006/api/QuanLyPhong/danh-sach-phong";

            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();


            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.PhongTrangChu>>();
                modelViewAdmin.PhongTrangChus = lisitems;
            }
            //modelViewUser.danhSachCheckPhong = new ModelViewAdmin.DanhSachCheckPhong();

            return modelViewAdmin;
        }
        [Route("cap-nhat-giam-gia")]
        public async Task<IActionResult> CapNhatGiamGia(string id) 
        {
            var item = await pvCapNhatGiamGia(id);
            return View(item);
        }
        private async Task<ModelViewAdmin> pvCapNhatGiamGia(string id)
        {
            var items = await pvDanhSachGiamGia();
            var item = items.PhongTrangChus.FirstOrDefault(c => c.IdPhong == id);
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            modelViewAdmin.PhongTrangChus=new List<ModelViewAdmin.PhongTrangChu> { item};
            
            return modelViewAdmin;
        }
        [Route("cap-nhat-giam-gia")]
        [HttpPost]
        public async Task<IActionResult> CapNhatGiamGia(string id, ModelViewAdmin.GiamGia input)
        {
            if (input.GiaPhong < input.GiaSauGiam)
            {
                TempData["error"] = "Giá sau giảm phải nhỏ hơn hoặc bằng giá phòng";
                return RedirectToAction("CapNhatGiamGia", "GiamGia", new { Areas = "Admin", id=id });
            }
           await pvCapNhatGiamGia(id,input);
            return RedirectToAction("DanhSachGiamGia", "GiamGia", new { Areas = "Admin"});
        }
        private async Task pvCapNhatGiamGia(string id, ModelViewAdmin.GiamGia input)
        {
            string url = "http://localhost:5006/api/GiamGia/quan-ly-giam-gia/"+id;
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(id), "id");
            var gg = System.Text.Json.JsonSerializer.Serialize(input);
            data.Add(new StringContent(gg), "gg");
            var res = await _httpClient.PatchAsync(url, data);
            if (res.IsSuccessStatusCode)
            {
            }
            
        }
    }
}
