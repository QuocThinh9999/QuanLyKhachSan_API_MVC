using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ClientQuanLyKhachSan.Areas.Admin.Models.TaiKhoan;
using ClientQuanLyKhachSan.Areas.Admin.Views.TaiKhoan;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using ClientQuanLyKhachSan.Areas.Admin.Models;
using ClientQuanLyKhachSan.Models;

namespace ClientQuanLyKhachSan.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaiKhoanController : Controller
    {
        private readonly HttpClient _httpClient;

        public TaiKhoanController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Client");
        }

        [Route("dang-nhap")]
        public async Task<IActionResult> DangNhap()
        {
            ModelViewUser modelViewUser = new ModelViewUser();
            return View(modelViewUser);
        }
        
        [Route("dang-nhap")]
        [HttpPost]
        public async Task<IActionResult> DangNhap(InputDangNhap input)
        {
            string url = "http://localhost:5006/api/Authentication/auth";
            ModelViewUser modelViewUser = new ModelViewUser();
            if (ModelState.IsValid)
            {
                var data = new MultipartFormDataContent();
                data.Add(new StringContent(input.Email), "Email");
                data.Add(new StringContent(EncryptPassword(input.Password)), "Password");

                var res = await _httpClient.PostAsync(url, data);
                if (res.IsSuccessStatusCode)
                {
                    var token = await res.Content.ReadAsAsync<OutputToken>();

                    if (token.TrangThai == 0)
                    {
                        return RedirectToAction("XacThuc1", "TaiKhoan", new { Areas = "Admin", id = token.IdNguoiDung });
                    }
                    if (token.TrangThai == 2)
                    {
                        TempData["error"] = "Tài khoản của bạn đã bị chặn";

                        return View(modelViewUser);
                    }
                    if (token.TrangThai == 3)
                    {
                        TempData["error"] = "Email hoặc mật khẩu không đúng";

                        return View(modelViewUser);
                    }
                    return await AccessLogin(token.Token);
                }
            }
            return View(modelViewUser);
        }
        private async Task<IActionResult> AccessLogin(string Token)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(Token) as JwtSecurityToken;
            var identity = new ClaimsIdentity(token.Claims, "Token");
            var principal = new ClaimsPrincipal(identity);

            var role = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var username = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            Response.Cookies.Append("Username", username);
            Response.Cookies.Append("Token", Token);
            Response.Cookies.Append("Role", role);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return await CheckRole(role);
        }


        private async Task<IActionResult> CheckRole(string? role)
        {
            if (role == "Admin") return RedirectToAction("DanhSachThongTinWeb", "QuanLyThongTinWeb", new { Areas = "Admin" });
            if (role == "User") return RedirectToAction("TrangChu","TrangChu", new { Areas = "" });
           
            return Redirect("/");
        }

        [Route("dang-ky")]
        public IActionResult DangKy()
        {
            var modelViewUser = new ModelViewUser();
            modelViewUser.InputDangKys=new ModelViewUser.InputDangKy();
            return View(modelViewUser);
        }

        [Route("dang-ky")]
        [HttpPost]
        public async Task<IActionResult> DangKy(ModelViewUser.InputDangKy input)
        {
            ModelViewUser modelViewUser = new ModelViewUser();

            modelViewUser.InputDangKys = input;
            string url = "http://localhost:5006/api/TaiKhoan/dang-ky";
            if (ModelState.IsValid)
            {
                if (input.Password != input.Password2)
                {
                    TempData["error"] = "Mật khẩu xác nhận không đúng";

                    return View(modelViewUser);
                }
                if (input.Password.Length < 4)
                {
                    TempData["error"] = "Mật khẩu phải từ 4 kí tự";
                    return View(modelViewUser);
                }
                var data = new MultipartFormDataContent();
                data.Add(new StringContent(input.Email), "Email");
                data.Add(new StringContent(input.Username), "Username");
                data.Add(new StringContent(EncryptPassword(input.Password)), "Password");


                var res = await _httpClient.PostAsync(url, data);
                //var result = await res.Content.ReadAsAsync<OutputDangKy>();
                if (res.IsSuccessStatusCode)
                {
                    var trangthai = await res.Content.ReadAsAsync<ModelViewUser.NguoiDungDangKy>();
                    if (trangthai.TrangThai == 0)
                    {
                        modelViewUser.inputOTP = new ModelViewUser.InputOTP();
                        modelViewUser.inputOTP.IdNguoiDung = trangthai.IdNguoiDung;

                        return RedirectToAction("XacThuc1", "TaiKhoan", new { Areas = "Admin", input = trangthai.IdNguoiDung, QuenMatKhau = 0 });
                    }
                    if (trangthai.TrangThai == 1)
                    {
                        TempData["error"] = "Email này đã được đăng ký";

                        return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin" });
                    }
                    if (trangthai.TrangThai == 2)
                    {
                        TempData["error"] = "Tài khoản của bạn đã bị chặn";

                        return View(modelViewUser);
                    }
                }
            }
            TempData["error"] = "Vui lòng nhập đủ thông tin";
            return View(modelViewUser);
        }
        [Route("xac-thuc")]
        public async Task<IActionResult> XacThuc1(string input, int QuenMatKhau)
        {
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser.inputOTP=new ModelViewUser.InputOTP();
            modelViewUser.inputOTP.IdNguoiDung = input;
            modelViewUser.inputOTP.QuenMatKhau = QuenMatKhau;
            pvXacThuc1(input);
            return View(modelViewUser);
        }
        private async void pvXacThuc1(string input)
        {
            string url = "http://localhost:5006/api/TaiKhoan/xac-thuc-1";
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(input), "IdNguoiDung");
            var res = await _httpClient.PostAsync(url, data);
        }
        [Route("xac-thuc")]
        [HttpPost]
        public async Task<IActionResult> XacThuc2(ModelViewUser.InputOTP input)
        {
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser.inputOTP = new ModelViewUser.InputOTP();
            modelViewUser.inputOTP = input;
            var items = await pvXacThuc2(input);
            if (items == 0)
            {
                TempData["error"] = "Mã xác thực không đúng";

                return View(modelViewUser);
            }
            if (input.QuenMatKhau == 1)
            {
                return RedirectToAction("DoiLaiMatKhau1", "TaiKhoan", new { Areas = "Admin", id=input.IdNguoiDung });
            }
            return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin" });
        }
        private async Task<int> pvXacThuc2(ModelViewUser.InputOTP input)
        {
            
            string url = "http://localhost:5006/api/TaiKhoan/xac-thuc-2";
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(input.IdNguoiDung), "IdNguoiDung");
            data.Add(new StringContent(input.MaXacThuc), "MaXacThuc");
            var res = await _httpClient.PostAsync(url, data);
            if (res.IsSuccessStatusCode)
            {
                var trangthai = await res.Content.ReadAsAsync<int>();
                return trangthai;
            }
            return 0;
        }
            private string EncryptPassword(string password)
        {
            //using (var sha256 = SHA256.Create())
            //{
            //    var data = Encoding.UTF8.GetBytes(password);
            //    var hash = sha256.ComputeHash(data);
            //    return Convert.ToBase64String(hash);
            //}
            using (var md5 = MD5.Create())
            {
                var data = Encoding.UTF8.GetBytes(password);
                var hash = md5.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }
        [Route("quen-mat-khau")]
        public async Task<IActionResult> QuenMatKhau()
        {
            ModelViewUser modelViewUser=new ModelViewUser();
            return View(modelViewUser);
        }
        [HttpPost]
        [Route("quen-mat-khau")]
        public async Task<IActionResult> QuenMatKhau(string email)
        {
            ModelViewUser modelViewUser = new ModelViewUser();
            string url = "http://localhost:5006/api/TaiKhoan/Quen-mat-khau";
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(email), "email");
            var res = await _httpClient.PostAsync(url, data);
            if (res.IsSuccessStatusCode)
            {
                var inputQMK = await res.Content.ReadAsAsync<ModelViewUser.InputQMK>();
                if(inputQMK.TrangThai==-1)
                {
                    TempData["error"] = "Email này chưa được đăng ký";
                    return View(modelViewUser);
                }
                if (inputQMK.TrangThai == 0)
                {
                    TempData["error"] = "Email này chưa được xác thực";
                    return View(modelViewUser);
                }
                if (inputQMK.TrangThai == 2)
                {
                    TempData["error"] = "Email này đã bị chặn";
                    return View(modelViewUser);
                }
                return RedirectToAction("XacThuc1", "TaiKhoan", new { Areas = "Admin", input = inputQMK.IdNguoiDung, QuenMatKhau = 1 });
            }
            return null;
        }
        
        [Route("doi-lai-mat-khau-1")]
        public async Task<IActionResult> DoiLaiMatKhau1(string id)
        {
            ModelViewUser modelViewUser = new ModelViewUser();
            modelViewUser.doiLaiMatKhau = new ModelViewUser.DoiLaiMatKhau();
            modelViewUser.doiLaiMatKhau.IdNguoiDung = id;
            return View(modelViewUser);
        }
        [HttpPost]
        [Route("doi-lai-mat-khau-2")]
        public async Task<IActionResult> DoiLaiMatKhau2(ModelViewUser.DoiLaiMatKhau input)
        {
            ModelViewUser modelViewUser=new ModelViewUser();
            modelViewUser.doiLaiMatKhau=new ModelViewUser.DoiLaiMatKhau();
            modelViewUser.doiLaiMatKhau = input;
            if (ModelState.IsValid)
            {
                if (input.Password != input.Password2)
                {
                    TempData["error"] = "Mật khẩu xác nhận không đúng";

                    return View(modelViewUser);
                }
                if (input.Password.Length < 4)
                {
                    TempData["error"] = "Mật khẩu phải từ 4 kí tự";
                    return View(modelViewUser);
                }
                pvDoiLaiMatKhau2(input);
                TempData["error"] = "Đổi mật khẩu thành công";
                return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin" });
            }
            TempData["error"] = "vui lòng nhập mật khẩu";
            return View(modelViewUser);
        }
        private async void pvDoiLaiMatKhau2(ModelViewUser.DoiLaiMatKhau input)
        {
            string url = "http://localhost:5006/api/TaiKhoan/Doi-lai-mat-khau";
            var data = new MultipartFormDataContent();
            data.Add(new StringContent(input.IdNguoiDung), "id");
            data.Add(new StringContent(EncryptPassword(input.Password)), "Password");
            var res = await _httpClient.PostAsync(url, data);
        }
        [Route("access-denied")]
        public IActionResult TuChoi()
        {
            return View();
        }

        [Route("logout")]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("Token");
            return RedirectToAction("DangNhap", "TaiKhoan", new { Areas = "Admin" });
        }
    }
}
