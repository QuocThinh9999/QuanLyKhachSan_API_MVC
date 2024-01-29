using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using APIQuanLyKhachSan.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly DbQuanLyKhachSanContext _context;
        public AuthenticationController(IConfiguration configuration, DbQuanLyKhachSanContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> XacThuc([FromForm] Login input )
        {
            var item = await _context.NguoiDungs.FirstOrDefaultAsync(c => c.Email == input.Email  
            && c.MatKhau == input.Password);

            if (item == null) {
                return Ok(new OutputToken
                {
                    TrangThai=3
                });
            }
            var xt= _context.XacThucs.FirstOrDefault(c=>c.IdNguoiDung==item.IdNguoiDung);
            
            if (xt.TrangThai == 2)
            {       
                return Ok(new OutputToken
                {
                    TrangThai=2
                });
            }
            if (xt.TrangThai == 0)
            {
                SendOTP(input.Email, item.IdNguoiDung);

                return Ok(new OutputToken
                {
                    TrangThai = 0,
                    IdNguoiDung=item.IdNguoiDung
                });
            }
            var token = GenerateJWT(item);
            return Ok(new OutputToken
            {
                Token = token,
                RefreshToken = null,
                InvokeToken = null,
                Times = null,
                TrangThai=1
            });
        }

        private string GenerateJWT(NguoiDung nguoidung)
        {
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256Signature);
            //var role = _context.Roles.FirstOrDefault(c => c.Id == taikhoan.Id);
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, nguoidung.IdNguoiDung),
                new Claim(ClaimTypes.Name, nguoidung.Ten),
                new Claim(ClaimTypes.Role,  nguoidung.IdQuyen),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Issuer"],
                claims,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(120)).DateTime,
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private IActionResult SendOTP(string email, string id)
        {
            // Tạo mã OTP ngẫu nhiên
            Random random = new Random();
            int otp = random.Next(100000, 999999);
            var xacthuc = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == id);
            xacthuc.MaXacThuc = otp.ToString();
            _context.Update(xacthuc);
            _context.SaveChanges();
            string imagePath = "D:\\Web\\QuanLyKhachSan\\wwwroot\\images\\MerPerle.png";

            string emailTemplate = @"
<html>
<body style='width:100%'>
    <h1>Xác thực tài khoản</h1>
    <h3>Mã xác thực: " + otp + @"</h3>
    <p class='text-danger'>Lưu ý: không chia sẻ mã xác thực cho bất kì ai, nếu không phải bạn, vui lòng bỏ qua email này</p>
<footer style='width:100%'>
    <img src='cid:footerImage' style='width:100%; height:100%; object-fit:contain' alt='Footer Image'>
</footer>
</body>
</html>";

            // Gửi email chứa mã OTP và ảnh footer
            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("khachsanminhminhueh@gmail.com", "vryudjggtixnykjb");
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("khachsanminhminhueh@gmail.com");
                    mailMessage.To.Add(email);
                    mailMessage.Subject = "OTP Verification";
                    mailMessage.Body = emailTemplate;
                    mailMessage.IsBodyHtml = true;

                    // Đính kèm hình ảnh
                    Attachment imageAttachment = new Attachment(imagePath);
                    imageAttachment.ContentId = "footerImage";
                    mailMessage.Attachments.Add(imageAttachment);

                    smtpClient.Send(mailMessage);
                }

                return Ok("OTP sent successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to send OTP: {ex.Message}");
            }
        }
    }
}
