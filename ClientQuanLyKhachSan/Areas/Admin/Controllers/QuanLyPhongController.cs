using ClientQuanLyKhachSan.Areas.Admin.Models;
using ClientQuanLyKhachSan.Common;
using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("quan-ly-phong")]
    public class QuanLyPhongController : Controller
    {
        private readonly HttpClient _httpClient;

        public QuanLyPhongController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("Danh-sach-phong")]
        public async Task<IActionResult> DanhSachPhong()
        {
            var items = await GetDanhSachPhong();
            return View(items);
        }
        [Route("Danh-sach-phong")]
        [HttpPost]
        public async Task<IActionResult> DanhSachPhong(string ten)
        {
            var items = await GetDanhSachPhong();
            items.PhongTrangChus = items.PhongTrangChus.Where(c => c.TenPhong.Contains((ten ?? "").ToLower())).ToList();
            return View(items);
        }
        private async Task<ModelViewAdmin> GetDanhSachPhong()
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
        [Route("xoa-phong")]
        public async Task<IActionResult> XoaPhong(string id)
        {

            await XoaAnh(id);
            await pvXoaPhong(id);
            return RedirectToAction("DanhSachPhong", "QuanLyPhong", new { Areas = "Admin" });
        }

        private async Task pvXoaPhong(string id)
        {
            string url = "http://localhost:5006/api/QuanLyPhong/xoa-phong/" + id;
            var res = await _httpClient.DeleteAsync(url);
        }
        [Route("cap-nhat-phong")]
        public async Task<IActionResult> CapNhatPhong(string id)
        {
            var item = await pvCapNhatPhong(id);
            return View(item);
        }
        private async Task<ModelViewAdmin> pvCapNhatPhong(string id)
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            string url1 = "http://localhost:5006/api/QuanLyPhong/chi-tiet-phong/" + id;
            var res = await _httpClient.GetAsync(url1);

            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<ModelViewAdmin.ChiTietTrangPhong>();
                modelViewAdmin.ChiTietTrangPhongs = lisitems;
            }
            return modelViewAdmin;
        }
        [Route("cap-nhat-phong")]
        [HttpPost]
        public async Task<IActionResult> CapNhatPhong(ModelViewAdmin.CapNhatPhong input)
        {
            if (input.TenPhong != null && input.GiaPhong != 0 && input.SoLuong != 0 && input.SoNguoiLon != 0 && input.DienTich != 0)
            {
                if (input.Images != null)
                {
                    await XoaAnh(input.id);
                }
                await pvCapNhatPhong(input);
                return RedirectToAction("DanhSachPhong", "QuanLyPhong", new { Areas = "Admin" });
            }
            TempData["error"] = "Vui lòng nhập đầy đủ thông tin";
            return RedirectToAction("CapNhatPhong", "QuanLyPhong", new { Areas = "Admin", id=input.id }); 
            
        }
        private async Task pvCapNhatPhong(ModelViewAdmin.CapNhatPhong input)
        {
            
            string url = "http://localhost:5006/api/QuanLyPhong/cap-nhat-phong/"+input.id;
            var data = new MultipartFormDataContent();
            if (input.Images != null)
            {
                
                input.UrlImages = new List<string>();
                foreach (var image in input.Images)
                {
                    input.UrlImage = UploadFiles.SaveImage(image);
                    input.UrlImages.Add(input.UrlImage);
                }
            }
            data.Add(new StringContent(input.id), "id");
            var cnp = System.Text.Json.JsonSerializer.Serialize(input);
            data.Add(new StringContent(cnp), "cnp");
            var res = await _httpClient.PutAsync(url, data);

        }
        private async Task XoaAnh(string id)
        {
            string url1 = "http://localhost:5006/api/QuanLyPhong/chi-tiet-phong/" +id;
            var res1 = await _httpClient.GetAsync(url1);
            if (res1.IsSuccessStatusCode)
            {
                var lisitems = await res1.Content.ReadAsAsync<ModelViewAdmin.ChiTietTrangPhong>();
                foreach (var item in lisitems.UrlImages)
                {
                    if(item!= "\\images\\no_image.jpg")
                    {
                        UploadFiles.RemoveImage(item);
                    }
                    
                }
            }
        }
        [Route("them-phong")]
        public async Task<IActionResult> ThemPhong()
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            return View(modelViewAdmin);
        }
        [Route("them-phong")]
        [HttpPost]
        public async Task<IActionResult> ThemPhong(ModelViewAdmin.CapNhatPhong input)
        {
            
            if(input.TenPhong!=null&&input.GiaPhong!=0&&input.SoLuong!=0&&input.SoNguoiLon!=0&&input.DienTich!=0)
            {
                await pvThemPhong(input);
                return RedirectToAction("DanhSachPhong", "QuanLyPhong", new { Areas = "Admin" });
            }
            TempData["error"] = "Vui lòng nhập đầy đủ thông tin";
            return RedirectToAction("ThemPhong", "QuanLyPhong", new { Areas = "Admin" }); ;
            
        }
        private async Task pvThemPhong(ModelViewAdmin.CapNhatPhong input)
        {
            string url = "http://localhost:5006/api/QuanLyPhong/them-phong";
            var data = new MultipartFormDataContent();
            if (input.Images != null)
            {
                input.UrlImages = new List<string>();
                foreach (var image in input.Images)
                {
                    string img = UploadFiles.SaveImage(image);
                    input.UrlImages.Add(img);
                }
            }
            var tp = System.Text.Json.JsonSerializer.Serialize(input);
            data.Add(new StringContent(tp), "tp");
            var res = await _httpClient.PostAsync(url, data);
        }
    }
}
