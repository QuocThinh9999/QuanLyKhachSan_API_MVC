using ClientQuanLyKhachSan.Areas.Admin.Models;
using ClientQuanLyKhachSan.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("quan-ly-tai-khoan")]
    public class QuanLyTaiKhoanController : Controller
    {
        private readonly HttpClient _httpClient;

        public QuanLyTaiKhoanController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("danh-sach-tai-khoan")]
        public async Task<IActionResult> DanhSachTaiKhoan()
        {
            var item = await pvDanhSachTaiKhoan();
            return View(item);
        }
        [Route("danh-sach-tai-khoan")]
        [HttpPost]
        public async Task<IActionResult> DanhSachTaiKhoan(string ten)
        {
            var items = await pvDanhSachTaiKhoan();
            items.taiKhoanNguoiDungs = items.taiKhoanNguoiDungs.Where(c => c.Name.Contains((ten ?? "").ToLower()) || c.Email.Contains((ten ?? "").ToLower())).ToList();
            return View(items);
        }
        private async Task<ModelViewAdmin> pvDanhSachTaiKhoan()
        {
            string url = "http://localhost:5006/api/DanhSachNguoiDung/danh-sach-nguoi-dung";

            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();


            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.TaiKhoanNguoiDung>>();
                modelViewAdmin.taiKhoanNguoiDungs = lisitems;
            }
          
            return modelViewAdmin;
        }
        [Route("cap-nhat-tai-khoan")]
        public async Task<IActionResult> CapNhatTaiKhoan(string id)
        {
            var item = await pvCapNhatTaiKhoan(id);
            return View(item);
        }
        private async Task<ModelViewAdmin> pvCapNhatTaiKhoan(string id)
        {
            string url = "http://localhost:5006/api/DanhSachNguoiDung/danh-sach-nguoi-dung/"+id;

            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<List<ModelViewAdmin.TaiKhoanNguoiDung>>();
                modelViewAdmin.taiKhoanNguoiDungs = lisitems;
            }
            return modelViewAdmin;
        }
        [Route("cap-nhat-tai-khoan")]
        [HttpPost]
        public async Task<IActionResult> CapNhatTaiKhoan(ModelViewAdmin.TaiKhoanNguoiDung input)
        {
            if (input.TrangThai == 0)
            {
                return RedirectToAction("DanhSachTaiKhoan", "QuanLyTaiKhoan", new { Areas = "Admin" });
            }
            await pvCapNhatTaiKhoan(input);
            return RedirectToAction("DanhSachTaiKhoan", "QuanLyTaiKhoan", new { Areas = "Admin" });
        }
        private async Task pvCapNhatTaiKhoan(ModelViewAdmin.TaiKhoanNguoiDung input)
        {
            string url = "http://localhost:5006/api/DanhSachNguoiDung/cap-nhat-tai-khoan/" + input.Id;

            ModelViewAdmin modelViewAdmin = new ModelViewAdmin();
            var data = new MultipartFormDataContent();
            var cntk = System.Text.Json.JsonSerializer.Serialize(input);
            data.Add(new StringContent(cntk), "cntk");
            
            var res = await _httpClient.PatchAsync(url, data);
            if (res.IsSuccessStatusCode)
            {
            }
        }
    }
}
