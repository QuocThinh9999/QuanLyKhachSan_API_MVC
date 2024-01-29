const url = 'http://localhost:5006/api/ThongTinWeb/danh-sach-thong-tin-web';

$.getJSON(url, function (lisitems) {
    var sdt = lisitems.find(c => c.loaiThongTin === "SĐT");
    if (sdt) {
        $('#sdtheader').text(sdt.moTa);
        $('#sdtmobile').text(sdt.moTa);
        $('#sdtfooter').text(sdt.moTa);
    }

    var email = lisitems.find(c => c.loaiThongTin === "Email");
    if (email) {
        $('#emailheader').text(email.moTa);
        $('#emailmobile').text(email.moTa);
        $('#emailfooter').text(email.moTa);
    }

    var diachi = lisitems.find(c => c.loaiThongTin === "Địa Chỉ");
    if (diachi) {
        $('#diachifooter').text(diachi.moTa);
    }

    var linkFacebook = lisitems.find(c => c.loaiThongTin === "Link Facebook");
    if (linkFacebook) {
        $('#LinkFacebookHeader').attr('href', linkFacebook.moTa);
        $('#LinkFacebookMobile').attr('href', linkFacebook.moTa);
        $('#LinkFacebookFooter').attr('href', linkFacebook.moTa);
    }

    var linkInstagram = lisitems.find(c => c.loaiThongTin === "Link Instagram");
    if (linkInstagram) {
        $('#LinkInstagramHeader').attr('href', linkInstagram.moTa);
        $('#LinkInstagramMobile').attr('href', linkInstagram.moTa);
        $('#LinkInstagramFooter').attr('href', linkInstagram.moTa);
    }

    var linkYouTube = lisitems.find(c => c.loaiThongTin === "Link YouTube");
    if (linkYouTube) {
        $('#LinkYouTubeHeader').attr('href', linkYouTube.moTa);
        $('#LinkYouTubeMobile').attr('href', linkYouTube.moTa);
        $('#LinkYouTubeFooter').attr('href', linkYouTube.moTa);
    }

    var linkLinkedin = lisitems.find(c => c.loaiThongTin === "Link Linkedin");
    if (linkLinkedin) {
        $('#LinkLinkedinHeader').attr('href', linkLinkedin.moTa);
        $('#LinkLinkedinMobile').attr('href', linkLinkedin.moTa);
        $('#LinkLinkedinFooter').attr('href', linkLinkedin.moTa);
    }

    var linkTwitter = lisitems.find(c => c.loaiThongTin === "Link Twitter");
    if (linkTwitter) {
        $('#LinkTwitterHeader').attr('href', linkTwitter.moTa);
        $('#LinkTwitterMobile').attr('href', linkTwitter.moTa);
        $('#LinkTwitterFooter').attr('href', linkTwitter.moTa);
    }

    var gioithieu = lisitems.find(c => c.loaiThongTin === "Giới thiệu");
    if (gioithieu) {
        $('#gioithieu').text(gioithieu.moTa);
    }

    var dulichcamtrai = lisitems.find(c => c.loaiThongTin === "Du Lịch & Cắm Trại");
    if (dulichcamtrai) {
        $('#DuLichCamTraiTC').attr('src', dulichcamtrai.urlImages);
    }

    var dichvunhahang = lisitems.find(c => c.loaiThongTin === "Dịch Vụ Nhà Hàng");
    if (dichvunhahang) {
        $('#DichVuNhaHangTC').attr('src', dichvunhahang.urlImages);
    }
}).fail(function (jqXHR, textStatus, errorThrown) {
    console.error('Lỗi khi gọi API:', textStatus, errorThrown);
});