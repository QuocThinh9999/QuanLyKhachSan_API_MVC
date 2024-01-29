namespace APIQuanLyKhachSan.Models.ModelView
{
    public class ChiTietBlog
    {
        public string IdBlog { get; set; } = null!;

        public List<string> UrlImages { get; set; }

        public string? MoTa { get; set; }

        public string? Tieude { get; set; }
    }
}
