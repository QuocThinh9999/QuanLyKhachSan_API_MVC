using ClientQuanLyKhachSan.Areas.Admin.Models;
using ClientQuanLyKhachSan.Common;
using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("quan-ly-giam-gia")]
    public class QuanLyBaiVietController : Controller
    {
        private readonly HttpClient _httpClient;

        public QuanLyBaiVietController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("danh-sach-bai-viet")]
        public async Task<IActionResult> DanhSachBaiViet()
        {
            var item =await pvDanhSachBaiViet();
            return View(item);
        }
        [Route("danh-sach-bai-viet")]
        [HttpPost]
        public async Task<IActionResult> DanhSachBaiViet(string ten)
        {
            var items = await pvDanhSachBaiViet();
            items.BlogTrangChus=items.BlogTrangChus.Where(c => c.Tieude.Contains((ten ?? "").ToLower())).ToList();
            return View(items);
        }
        private async Task<ModelViewAdmin> pvDanhSachBaiViet()
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            string url = "http://localhost:5006/api/QuanLyBaiViet/danh-sach-bai-viet";
            

            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.BlogTrangChu>>();
                modelViewAdmin.BlogTrangChus = lisitems;
            }
            return modelViewAdmin;
        }
        [Route("xoa-bai-viet")]
        public async Task<IActionResult> XoaBaiViet(string id)
        {
            await pvXoaBaiViet(id);
            return RedirectToAction("DanhSachBaiViet", "QuanLyBaiViet", new { Areas = "Admin" });
        }
        private async Task pvXoaBaiViet(string id)
        {
            string url = "http://localhost:5006/api/QuanLyBaiViet/xoa-bai-viet/"+id;
            await _httpClient.DeleteAsync(url);
        }
        [Route("them-bai-viet")]
        public async Task<IActionResult> ThemBaiViet()
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            return View(modelViewAdmin);
        }
        [Route("them-bai-viet")]
        [HttpPost]
        public async Task<IActionResult> ThemBaiViet(ModelViewAdmin.ThemBaiViet input)
        {
            if (input.Tieude != null && input.MoTa != null)
            {
                await pvThemBaiViet(input);
                return RedirectToAction("DanhSachBaiViet", "QuanLyBaiViet", new { Areas = "Admin" });
            }
            TempData["error"] = "Vui lòng nhập đầy đủ thông tin";
            return RedirectToAction("ThemBaiViet", "QuanLyBaiViet", new { Areas = "Admin" }); ;
        }
        private async Task pvThemBaiViet(ModelViewAdmin.ThemBaiViet input)
        {
           
            string url = "http://localhost:5006/api/QuanLyBaiViet/them-bai-viet";
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
            var tbv = System.Text.Json.JsonSerializer.Serialize(input);
            data.Add(new StringContent(tbv), "tbv");
            var res = await _httpClient.PostAsync(url, data);
            
        }
        [Route("cap-nhat-bai-viet")]
        public async Task<IActionResult> CapNhatBaiViet(string id)
        {
            var item = await pvCapNhatBaiViet(id);
            return View(item);
        }
        private async Task<ModelViewAdmin> pvCapNhatBaiViet(string id)
        {
            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            string url1 = "http://localhost:5006/api/QuanLyBaiViet/chi-tiet-bai-viet/" + id;
            var res = await _httpClient.GetAsync(url1);

            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<ModelViewAdmin.BlogTrangChu>();
                modelViewAdmin.blogTrangChu = lisitems;
            }
            return modelViewAdmin;
        }
        [Route("cap-nhat-bai-viet")]
        [HttpPost]
        public async Task<IActionResult> CapNhatBaiViet(ModelViewAdmin.ThemBaiViet input)
        {
            if (input.Tieude != null && input.MoTa != null)
            {
                if (input.Images != null)
                {
                    await XoaAnh(input.id);
                }
                await pvCapNhatBaiViet(input);
                return RedirectToAction("DanhSachBaiViet", "QuanLyBaiViet", new { Areas = "Admin" });
            }
            TempData["error"] = "Vui lòng nhập đầy đủ thông tin";
            return RedirectToAction("CapNhatBaiViet", "QuanLyBaiViet", new { Areas = "Admin", id=input.id }); ;
           
        }
        private async Task pvCapNhatBaiViet(ModelViewAdmin.ThemBaiViet input)
        {

            string url = "http://localhost:5006/api/QuanLyBaiViet/cap-nhat-bai-viet/" + input.id;
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
            var cnbv = System.Text.Json.JsonSerializer.Serialize(input);
            data.Add(new StringContent(cnbv), "cnbv");
            var res = await _httpClient.PutAsync(url, data);

        }
        private async Task XoaAnh(string id)
        {
            string url1 = "http://localhost:5006/api/QuanLyBaiViet/chi-tiet-bai-viet/" + id;
            var res1 = await _httpClient.GetAsync(url1);
            if (res1.IsSuccessStatusCode)
            {
                var lisitems = await res1.Content.ReadAsAsync<ModelViewAdmin.ThemBaiViet>();
                foreach (var item in lisitems.UrlImages)
                {
                    if (item != "\\images\\no_image.jpg")
                    {
                        UploadFiles.RemoveImage(item);
                    }

                }
            }
        }
    }
}
