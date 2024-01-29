using System.Collections.Generic;
using System.Text.Json;
using APIQuanLyKhachSan.Models;
using APIQuanLyKhachSan.Models.ModelView;
using APIQuanLyKhachSan.Models.ThemDuLieu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIQuanLyKhachSan.Common;
using WebQuanLyKhachSan.Models.ThemDuLieu;


namespace APIQuanLyKhachSan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class QuanLyPhongController : ControllerBase
    {
        private readonly DbQuanLyKhachSanContext _dbContext;
        public QuanLyPhongController(DbQuanLyKhachSanContext context)
        {
            _dbContext = context;
        }
        [AllowAnonymous]
        [HttpGet("danh-sach-phong")]
        public IActionResult DanhSachPhong()
        {
            List<PhongTrangChu> phongTrangChus = new List<PhongTrangChu>();
            var phongs = _dbContext.Phongs.ToList();

            foreach (var p in phongs)
            {
                PhongTrangChu phongTrangChu = new PhongTrangChu();
                var ctp = _dbContext.ChiTietPhongs.FirstOrDefault(c => c.IdPhong == p.Id);
                phongTrangChu.IdPhong = p.Id;
                phongTrangChu.TenPhong = p.TenPhong;
                phongTrangChu.GiaPhong = p.GiaPhong;
                phongTrangChu.GiaSauGiam = p.GiaSauGiam;
                phongTrangChu.SoNguoiLon = ctp.SoNguoiLon;
                phongTrangChu.SoTreEm = ctp.SoTreEm;
                phongTrangChu.DienTich = ctp.DienTich;
                phongTrangChu.MoTa = ctp.MoTa;
                var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(p.UrlImage);
                phongTrangChu.UrlImage = ListUrl[0].UrlImage;
                phongTrangChus.Add(phongTrangChu);
            }
            phongTrangChus = phongTrangChus.OrderBy(c => Guid.NewGuid()).ToList();

            return Ok(phongTrangChus);
        }
        [AllowAnonymous]
        [HttpGet("chi-tiet-phong/{id}")]
        public IActionResult ChiTietPhong(string id)
        {
            var phongc = _dbContext.Phongs.FirstOrDefault(c => c.Id == id);
            var ctp = _dbContext.ChiTietPhongs.FirstOrDefault(c => c.IdPhong == id);
            var phong = new ChiTietTrangPhong();
            phong.id = phongc.Id;
            phong.TenPhong = phongc.TenPhong;
            phong.SoLuong = phongc.SoLuong;
            phong.GiaPhong = phongc.GiaPhong;
            phong.GiaSauGiam = phongc.GiaSauGiam;
            phong.SoNguoiLon = ctp.SoNguoiLon;
            phong.SoTreEm = ctp.SoTreEm;
            phong.DienTich = ctp.DienTich;
            phong.MoTa = ctp.MoTa;
            var ListUrl = System.Text.Json.JsonSerializer.Deserialize<List<OutputImage>>(phongc.UrlImage);
            phong.UrlImages = new List<string>();
            foreach (var url in ListUrl)
            {
                phong.UrlImages.Add(url.UrlImage);
            }
            var items = CheckPhong(id);
            phong.ngayKhongKhaDung = new List<ChiTietTrangPhong.NgayKhongKhaDung>();
            foreach (var item in items)
            {
                var nkkd = new ChiTietTrangPhong.NgayKhongKhaDung();
                nkkd.GioCheckin = item.GioCheckin;
                nkkd.GioCheckout = item.GioCheckout.AddDays(-1);
                phong.ngayKhongKhaDung.Add(nkkd);
            }

            return Ok(phong);
        }
        private List<CheckPhong.NgayKhongKhaDung> CheckPhong(string id)
        {
            var phong = _dbContext.Phongs.FirstOrDefault(c => c.Id == id);
            var hoadons = _dbContext.HoaDons.Where(c => c.IdPhong == phong.Id && c.TrangThai == "Chờ nhận phòng").ToList();
            var checkphong = new List<CheckPhong.NgayKhongKhaDung>();
            var checkphong2 = new List<CheckPhong.NgayKhongKhaDung>();
            if (hoadons.Count == 0)
            {
                return checkphong;
            }
            if (phong.SoLuong == 1)
            {
                foreach (var item in hoadons)
                {
                    var cp = new Models.ModelView.CheckPhong.NgayKhongKhaDung();
                    cp.GioCheckin = item.GioCheckin;
                    cp.GioCheckout = item.GioCheckout;
                    checkphong.Add(cp);
                }
                return checkphong;
            }
            if (hoadons.Count == 1)
            {
                if (phong.SoLuong > 1) return checkphong;
                var nkkd = new CheckPhong.NgayKhongKhaDung();
                nkkd.GioCheckin = hoadons[0].GioCheckin;
                nkkd.GioCheckout = hoadons[0].GioCheckout;
                checkphong.Add(nkkd);
                return checkphong;
            }
            var checktam = new CheckPhong.NgayKhongKhaDung();
            var tohop = LayTatCaTongHop(phong.SoLuong, hoadons);
            //var check = CheckNgay(hoadons, phong.SoLuong, 0, 0, checktam, checkphong2);
            //var check = CheckNgay(hoadons, phong.SoLuong);
            foreach (var item in tohop)
            {
                var itemm = CheckNgay(item, phong.SoLuong);
                if (itemm.GioCheckin.Year != 1)
                {
                    checkphong.Add(itemm);
                }
                
            }
            return checkphong;
        }

        //private List<CheckPhong.NgayKhongKhaDung> CheckNgay(List<HoaDon> hoadons, int? soPhong, int start, int check, CheckPhong.NgayKhongKhaDung checkTam, List<CheckPhong.NgayKhongKhaDung> checkPhongs)
        //{

        //    if (check == soPhong)
        //    {
        //        checkPhongs.Add(checkTam);
        //        checkTam = new CheckPhong.NgayKhongKhaDung();
        //        check = 0;
        //    }
        //    for (int i = start; i < hoadons.Count; i++)
        //    {
        //        if (check == 0)
        //        {
        //            checkTam.GioCheckin = hoadons[i].GioCheckin;
        //            checkTam.GioCheckout = hoadons[i].GioCheckout;
        //            return CheckNgay(hoadons, soPhong, i + 1, check + 1, checkTam, checkPhongs);
        //        }
        //        if (hoadons[i].GioCheckin <= checkTam.GioCheckin && checkTam.GioCheckout <= hoadons[i].GioCheckout)
        //        {
        //            return CheckNgay(hoadons, soPhong, i + 1, check + 1, checkTam, checkPhongs);
        //        }
        //        if (hoadons[i].GioCheckin <= checkTam.GioCheckin && checkTam.GioCheckin < hoadons[i].GioCheckout && hoadons[i].GioCheckout <= checkTam.GioCheckout)
        //        {
        //            checkTam.GioCheckout = hoadons[i].GioCheckout;
        //            return CheckNgay(hoadons, soPhong, i + 1, check + 1, checkTam, checkPhongs);
        //        }
        //        if (checkTam.GioCheckin <= hoadons[i].GioCheckin && hoadons[i].GioCheckin < checkTam.GioCheckout && hoadons[i].GioCheckout >= checkTam.GioCheckout)
        //        {
        //            checkTam.GioCheckin = hoadons[i].GioCheckin;
        //            return CheckNgay(hoadons, soPhong, i + 1, check + 1, checkTam, checkPhongs);
        //        }

        //    }
        //    return checkPhongs;
        //}
        private CheckPhong.NgayKhongKhaDung CheckNgay(List<HoaDon> hoadons, int soPhong)
        {
            
            CheckPhong.NgayKhongKhaDung checkPhongs = new CheckPhong.NgayKhongKhaDung();

          
            var nkkd = new List<DateTime>();
            var chonnkkd = new List<int>();
            for (DateTime date = hoadons[0].GioCheckin.Date; date <= hoadons[0].GioCheckout.Date; date = date.AddDays(1))
            {
                nkkd.Add(date);
                chonnkkd.Add(1);
            }
            for(int i=1;i<hoadons.Count;i++ )
            {
                if (hoadons[i].GioCheckin <= hoadons[0].GioCheckin && hoadons[0].GioCheckout <= hoadons[i].GioCheckout)
                {
                    for (int j = 0; j < chonnkkd.Count; j++)
                    {
                        chonnkkd[j] += 1;
                    }
                }
                if (hoadons[i].GioCheckin <= hoadons[0].GioCheckin && hoadons[0].GioCheckin < hoadons[i].GioCheckout && hoadons[i].GioCheckout <= hoadons[0].GioCheckout)
                {
                  for(int j=0;j<nkkd.Count;j++)
                    {
                        if (hoadons[i].GioCheckout == nkkd[j].Date)
                        {
                            for(int k = 0; k < j; k++)
                            {
                                chonnkkd[k] += 1;
                            }
                        }
                    }
                   
                }
                if (hoadons[0].GioCheckin <= hoadons[i].GioCheckin && hoadons[i].GioCheckin < hoadons[0].GioCheckout && hoadons[i].GioCheckout >= hoadons[0].GioCheckout)
                {
                    for (int j = 0; j < nkkd.Count; j++)
                    {
                        if (hoadons[i].GioCheckin == nkkd[j].Date)
                        {
                            for (int k = chonnkkd.Count-1; k >j; k--)
                            {
                                chonnkkd[k] += 1;
                            }
                        }
                    }

                }
            }
            var nkkd2 = new List<DateTime>();
            for (int j = 0; j < chonnkkd.Count; j++)
            {
                if (chonnkkd[j] >= soPhong)
                {
                    nkkd2.Add(nkkd[j]);
                }
            }
            if (nkkd2.Count != 0)
            {
                checkPhongs.GioCheckin = nkkd2[0];
                checkPhongs.GioCheckout = nkkd2[nkkd2.Count - 1];
            }
            
            return checkPhongs;
        }
        private List<List<HoaDon>> LayTatCaTongHop(int k, List<HoaDon> hoadons)
        {
            List<List<HoaDon>> tatCaTongHop = new List<List<HoaDon>>();
            List<HoaDon> tongHopHienTai = new List<HoaDon>();

            LayTatCaTongHopHelper(k, hoadons, 0, tongHopHienTai, tatCaTongHop);

            return tatCaTongHop;
        }

        private void LayTatCaTongHopHelper(int k, List<HoaDon> hoadons, int index, List<HoaDon> tongHopHienTai, List<List<HoaDon>> tatCaTongHop)
        {
            if (tongHopHienTai.Count == k)
            {
                tatCaTongHop.Add(new List<HoaDon>(tongHopHienTai));
                return;
            }

            for (int i = index; i < hoadons.Count; i++)
            {
                tongHopHienTai.Add(hoadons[i]);
                LayTatCaTongHopHelper(k, hoadons, i + 1, tongHopHienTai, tatCaTongHop);
                tongHopHienTai.RemoveAt(tongHopHienTai.Count - 1);
            }
        }
        [HttpDelete("xoa-phong/{id}")]
        public IActionResult XoaPhong( string id)
        {
            var phong = _dbContext.Phongs.FirstOrDefault(k => k.Id == id);
            if (phong != null)
            {
                var chitietphong = _dbContext.ChiTietPhongs.FirstOrDefault(ctp => ctp.IdPhong == phong.Id);
                _dbContext.ChiTietPhongs.Remove(chitietphong);
                var hoadon = _dbContext.HoaDons.Where(c => c.IdPhong == phong.Id).ToList();
                foreach (var hd in hoadon)
                {
                    hd.UrlImages = "\\images\\no_image.jpg";
                    _dbContext.HoaDons.Update(hd);
                }
                
                _dbContext.Phongs.Remove(phong);
                _dbContext.SaveChanges();
                return Ok();
            }
            else return BadRequest();
        }
        [HttpPost("them-phong")]
        public IActionResult Themphong([FromForm] string tp)
        {
            var input = System.Text.Json.JsonSerializer.Deserialize<ThemPhong>(tp);
            if (ModelState.IsValid)
            {
                string id = Guid.NewGuid().ToString();
                Phong phong = new Phong
                {
                    Id = id,
                    TenPhong = input.TenPhong,
                    SoLuong = input.SoLuong,
                    GiaPhong = input.GiaPhong,
                    GiaSauGiam = input.GiaPhong,
                };
                List<OutputImage> listimage = new List<OutputImage>();
                if(input.UrlImages!=null)
                {
                    foreach (var img in input.UrlImages)//
                    {
                        OutputImage img2 = new OutputImage();
                        img2.Position = 1;
                        img2.UrlImage = img;
                        listimage.Add(img2);
                    }
                }
                else 
                {
                    OutputImage img2 = new OutputImage();
                    img2.Position = 1;
                    img2.UrlImage = "\\images\\no_image.jpg";
                    listimage.Add(img2);
                }
                phong.UrlImage = JsonSerializer.Serialize(listimage);
                _dbContext.Phongs.Add(phong);
                ChiTietPhong chiTietPhong = new ChiTietPhong
                {
                    IdPhong = id,
                    SoNguoiLon = input.SoNguoiLon,
                    SoTreEm = input.SoTreEm,
                    DienTich = input.DienTich,
                    MoTa = input.MoTa,

                };
                _dbContext.ChiTietPhongs.Add(chiTietPhong);
                _dbContext.SaveChanges();
                return Ok();
            }
            else return BadRequest();

        }
        [HttpPut("cap-nhat-phong/{id}")]
        public IActionResult CapNhatPhong([FromForm] string id, [FromForm] string cnp)
        {
            if (ModelState.IsValid)
            {
                var input = System.Text.Json.JsonSerializer.Deserialize<ThemPhong>(cnp);
                var item = _dbContext.Phongs.FirstOrDefault(ph => ph.Id == id);
                if (item == null)
                {
                    return NotFound();
                }
                else
                {
                    item.TenPhong = input.TenPhong;
                    item.SoLuong = input.SoLuong;
                    item.GiaPhong = input.GiaPhong;
                }

                var ctp = _dbContext.ChiTietPhongs.FirstOrDefault(c => c.IdPhong == id);
                if (ctp == null)
                {
                    return NotFound();
                }
                else
                {
                    ctp.SoNguoiLon = input.SoNguoiLon;
                    ctp.SoTreEm = input.SoTreEm;
                    ctp.DienTich = input.DienTich;
                    ctp.MoTa = input.MoTa;
                }
                if(input.UrlImages != null)
                {
                    List<OutputImage> listimage = new List<OutputImage>();
                    foreach (var img in input.UrlImages)
                    {
                        OutputImage img2 = new OutputImage();
                        img2.Position = 1;
                        img2.UrlImage = img;
                        listimage.Add(img2);
                        
                    }
                    item.UrlImage = JsonSerializer.Serialize(listimage);
                }
               
                _dbContext.Update(item);
                _dbContext.Update(ctp);
                _dbContext.SaveChanges();
                return Ok();
            }
            else return BadRequest();
        }
    }
}
