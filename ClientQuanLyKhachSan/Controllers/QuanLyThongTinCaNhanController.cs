using ClientQuanLyKhachSan.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClientQuanLyKhachSan.Controllers
{
    public class QuanLyThongTinCaNhanController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuanLyThongTinCaNhanController(IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient.CreateClient("Client");
            _httpContextAccessor = httpContextAccessor;
        }
        [Route("cap-nhat-thong-tin-ca-nhan")]
        public async Task<IActionResult> CapNhatThongTinCaNhan()
        {
            var items = await GetCapNhatThongTinCaNhan();
            return View(items);
        }
        private async Task<ModelViewUser> GetCapNhatThongTinCaNhan()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Cookies["Token"];
            string id = GetUserIdFromToken(token);
            string url = "http://localhost:5006/api/QuanLyThongTinCaNhan/xem-thong-tin-ca-nhan/"+id;
            ModelViewUser modelViewUser = new ModelViewUser();
            var res = await _httpClient.GetAsync(url);
            if (res.IsSuccessStatusCode)
            {

                var lisitems = await res.Content.ReadAsAsync<ModelViewUser.NguoiDung>();
                modelViewUser.nguoiDung = lisitems;
            }

            modelViewUser.TrangThaiCapNhat = 0;
            return modelViewUser;
        }
        [Route("cap-nhat-thong-tin-ca-nhan")]
        [HttpPost]
        public async Task<IActionResult> CapNhatThongTinCaNhan(ModelViewUser.NguoiDung input)
        {
            if (input.Ten == null)
            {
                TempData["error"] = "Không được để trống tên";
                ModelViewUser modelViewUser=new ModelViewUser();
                modelViewUser.nguoiDung=input;
                return View(modelViewUser);
            }
            var items = await PostCapNhatThongTinCaNhan(input);
            return View(items);
        }
        private async Task<ModelViewUser> PostCapNhatThongTinCaNhan(ModelViewUser.NguoiDung input)
        {
            ModelViewUser modelViewUser = new ModelViewUser();
            var httpContext = _httpContextAccessor.HttpContext;
            var token = httpContext.Request.Cookies["Token"];
            string id = GetUserIdFromToken(token);
            string url = "http://localhost:5006/api/QuanLyThongTinCaNhan/cap-nhat-thong-tin-ca-nhan/" + id;
            var data = new MultipartFormDataContent();
            modelViewUser.nguoiDung = input;
            modelViewUser.nguoiDung.IdNguoiDung = id;
            var ttcn = System.Text.Json.JsonSerializer.Serialize(modelViewUser.nguoiDung);
            data.Add(new StringContent(ttcn), "ttcn");
            var res = await _httpClient.PostAsync(url, data);
            modelViewUser.TrangThaiCapNhat = 1;
            return modelViewUser;
        }
            private string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            // Trích xuất id từ payload
            var id = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid")?.Value;
            return id;
        }

    }
}
