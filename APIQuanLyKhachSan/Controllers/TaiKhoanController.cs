using APIQuanLyKhachSan.Models.Authentication;
using APIQuanLyKhachSan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using APIQuanLyKhachSan.Models.ModelView;

namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TaiKhoanController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _context;
        public TaiKhoanController(DbQuanLyKhachSanContext context)
        {
            _context = context;
        }

        [HttpPost("dang-ky")]
        public async Task<IActionResult> DangKy([FromForm] InputUser input)
        {
            var nddk = await pvDangKy(input);
            return Ok(nddk);
        }
        private async Task<NguoiDungDangKy> pvDangKy([FromForm] InputUser input)
        {
            var item = await _context.NguoiDungs.FirstOrDefaultAsync(c => c.Email == input.Email);
            if (item != null)
            {
                var xt = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == item.IdNguoiDung);
                NguoiDungDangKy nddky = new NguoiDungDangKy();
                nddky.IdNguoiDung = item.IdNguoiDung;
                nddky.TrangThai = xt.TrangThai;
                return nddky;
            }

            NguoiDung taiKhoan = new NguoiDung();
            taiKhoan.IdNguoiDung = Guid.NewGuid().ToString();
            taiKhoan.Email = input.Email;
            taiKhoan.Ten = input.Username;
            taiKhoan.MatKhau = input.Password;
            taiKhoan.IdQuyen = "User";
            XacThuc xacThuc = new XacThuc();
            xacThuc.IdNguoiDung = taiKhoan.IdNguoiDung;
            xacThuc.TrangThai = 0;

            _context.NguoiDungs.Add(taiKhoan);
            _context.XacThucs.Add(xacThuc);
            _context.SaveChanges();
            SendOTP(input.Email, taiKhoan.IdNguoiDung);
            NguoiDungDangKy nddk = new NguoiDungDangKy();
            nddk.IdNguoiDung = taiKhoan.IdNguoiDung;
            nddk.TrangThai = 0;
            return nddk;
        }
        [HttpPost("xac-thuc-1")]
        public IActionResult XacThuc1([FromForm] InputOTP input)
        {
            var nd = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == input.IdNguoiDung);
            SendOTP(nd.Email, input.IdNguoiDung);
            return Ok(input);
        }
        [HttpPost("xac-thuc-2")]
        public IActionResult XacThuc2([FromForm] InputOTP input)
        {
            var xacthuc2 = pvXacThuc2(input);
            return Ok(xacthuc2);
        }
        private int pvXacThuc2([FromForm] InputOTP input)
        {
            var item = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == input.IdNguoiDung);
            int i = 2;
            if (ModelState.IsValid)
            {

                if (input.MaXacThuc == item.MaXacThuc)
                {
                    item.TrangThai = 1;
                    _context.Update(item);
                    _context.SaveChanges();
                    i = 1;
                    return i;
                }
                else
                {
                    i = 0;
                    return i;
                }

            }

            return i;
        }
        [HttpPost("Quen-mat-khau")]
        public IActionResult QuenMatKhauEmail([FromForm] string email)
        {
            InputQMK inputQMK = pvQuenMatKhauEmail(email);
            return Ok(inputQMK);
        }
        private InputQMK pvQuenMatKhauEmail(string email)
        {
            var nd = _context.NguoiDungs.FirstOrDefault(c => c.Email == email);
            InputQMK inputQMK = new InputQMK();
            if (nd == null)
            {
                inputQMK.TrangThai = -1;
                return inputQMK;
            }
            var xt = _context.XacThucs.FirstOrDefault(c => c.IdNguoiDung == nd.IdNguoiDung);
            if (xt.TrangThai == 0 || xt.TrangThai == 2)
            {
                inputQMK.TrangThai = xt.TrangThai;
                return inputQMK;
            }
            inputQMK.TrangThai = 1;
            inputQMK.email = email;
            inputQMK.IdNguoiDung = nd.IdNguoiDung;

            return inputQMK;
        }
        [HttpPost("Doi-lai-mat-khau")]
        public IActionResult DoiLaiMatKhau([FromForm] string id, [FromForm] string Password)
        {
            var nd = _context.NguoiDungs.FirstOrDefault(c => c.IdNguoiDung == id);
            nd.MatKhau = Password;
            _context.Update(nd);
            _context.SaveChanges();
            return Ok();
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
