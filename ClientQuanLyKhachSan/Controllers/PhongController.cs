using System.Net.Http;
using System;
using System.Security.Policy;
using ClientQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Mvc;
using Humanizer.Localisation.TimeToClockNotation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClientQuanLyKhachSan.Controllers
{

    public class PhongController : Controller
    {
        private readonly HttpClient _httpClient;

        public PhongController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }
        [Route("danh-sach-phong")]
        public async Task<IActionResult> DanhSachPhong(string phongTrangChu)
        {
            if(phongTrangChu!=null)
            {
                var dscp = System.Text.Json.JsonSerializer.Deserialize<ModelViewUser.DanhSachCheckPhong>(phongTrangChu);
                var phong = System.Text.Json.JsonSerializer.Deserialize<List<ModelViewUser.PhongTrangChu>>(dscp.Phongs);
                ModelViewUser modelViewUser = new ModelViewUser();
                modelViewUser.PhongTrangChus = phong;
                modelViewUser.danhSachCheckPhong = new ModelViewUser.DanhSachCheckPhong();
                modelViewUser.danhSachCheckPhong = dscp;
                return View(modelViewUser);
            }
            var items = await GetDanhSachPhong();
            return View(items);
        }
        private async Task<ModelViewUser> GetDanhSachPhong()
        {
            string url = "http://localhost:5006/api/QuanLyPhong/danh-sach-phong";

            ModelViewUser modelViewUser = new ModelViewUser();
         
            
                var res = await _httpClient.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {

                    var lisitems = await res.Content.ReadAsAsync<List<ModelViewUser.PhongTrangChu>>();
                    modelViewUser.PhongTrangChus = lisitems;
                }
            modelViewUser.danhSachCheckPhong=new ModelViewUser.DanhSachCheckPhong();
            
            return modelViewUser;
        }
        [Route("chi-tiet-phong")]
        public async Task<IActionResult> ChiTietPhong(string id)
        {
            var items = await GetChiTietPhong(id);
            items.nkkd = new List<DateTime>();
            foreach (var item in items.ChiTietTrangPhongs.ngayKhongKhaDung)
            {
                DateTime GioCheckin = item.GioCheckin;
                DateTime GioCheckout = item.GioCheckout;

                // Lặp qua từng ngày từ GioCheckin đến GioCheckout
                for (DateTime date = GioCheckin; date <= GioCheckout; date = date.AddDays(1))
                {
                   
                    items.nkkd.Add(date);
                }
                //items.JonNkkd= System.Text.Json.JsonSerializer.Serialize(items.nkkd);
            }
            return View(items);
        }
        private async Task<ModelViewUser> GetChiTietPhong(string id)
        {
            string url1 = "http://localhost:5006/api/QuanLyPhong/chi-tiet-phong/"+id;

            ModelViewUser modelViewUser = new ModelViewUser();
            var res = await _httpClient.GetAsync(url1);

            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<ModelViewUser.ChiTietTrangPhong>();
                modelViewUser.ChiTietTrangPhongs = lisitems;
            }

            string url2 = "http://localhost:5006/api/QuanLyPhong/danh-sach-phong";

            var res2 = await _httpClient.GetAsync(url2);
            if (res2.IsSuccessStatusCode)
            {

                var lisitems = await res2.Content.ReadAsAsync<List<ModelViewUser.PhongTrangChu>>();
                modelViewUser.PhongTrangChus = lisitems;
            }

            
            return modelViewUser;
        }
        [Route("danh-sach-check-phong")]
        [HttpPost]
        public async Task<IActionResult> DanhSachCheckPhong(ModelViewUser.DanhSachCheckPhong input)
        {
            if(input.GioCheckin.ToString()=="01/01/0001 12:00:00 AM"|| input.GioCheckout.ToString() == "01/01/0001 12:00:00 AM")
            {
                TempData["error"] = "Vui lòng chọn đủ ngày giờ CheckIn, CheckOut!";

                return RedirectToAction("DanhSachPhong", "Phong");
            }
            var items = await GetDanhSachCheckPhong(input);
            var phong = System.Text.Json.JsonSerializer.Serialize(items.PhongTrangChus);
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser.danhSachCheckPhong = input;
            modelViewUser.danhSachCheckPhong.Phongs= phong;
            var dscp = System.Text.Json.JsonSerializer.Serialize(modelViewUser.danhSachCheckPhong);
            return RedirectToAction("DanhSachPhong", "Phong", new {phongTrangChu=dscp});
        }
        private async Task<ModelViewUser> GetDanhSachCheckPhong(ModelViewUser.DanhSachCheckPhong input)
        {
            string url = "http://localhost:5006/api/CheckPhong/danh-sach-check-phong";
            var data = new MultipartFormDataContent();
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser.danhSachCheckPhong = input;
            
            var dscp = System.Text.Json.JsonSerializer.Serialize(modelViewUser.danhSachCheckPhong);
            
            data.Add(new StringContent(dscp), "dscp");
            var res = await _httpClient.PostAsync(url, data);
            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<List<ModelViewUser.PhongTrangChu>>();
                modelViewUser.PhongTrangChus = lisitems;
            }

            return modelViewUser;
        }
    }
}
