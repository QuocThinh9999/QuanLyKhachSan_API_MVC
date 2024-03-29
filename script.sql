USE [master]
GO
/****** Object:  Database [dbQuanLyKhachSan]    Script Date: 29/01/2024 7:48:48 PM ******/
CREATE DATABASE [dbQuanLyKhachSan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyKhachSan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuanLyKhachSan.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyKhachSan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\QuanLyKhachSan_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [dbQuanLyKhachSan] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbQuanLyKhachSan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbQuanLyKhachSan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET  MULTI_USER 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbQuanLyKhachSan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbQuanLyKhachSan] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [dbQuanLyKhachSan] SET QUERY_STORE = ON
GO
ALTER DATABASE [dbQuanLyKhachSan] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [dbQuanLyKhachSan]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnhQuangCao]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnhQuangCao](
	[IdAnh] [nvarchar](450) NOT NULL,
	[UrlImageQC] [nvarchar](max) NULL,
 CONSTRAINT [PK_AnhQuangCao] PRIMARY KEY CLUSTERED 
(
	[IdAnh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[IdBlog] [nvarchar](450) NOT NULL,
	[UrlImage] [nvarchar](max) NULL,
	[MoTa] [nvarchar](max) NULL,
	[Tieude] [nvarchar](max) NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[IdBlog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietPhong]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhong](
	[IdPhong] [nvarchar](450) NOT NULL,
	[SoNguoiLon] [int] NULL,
	[SoTreEm] [int] NULL,
	[DienTich] [int] NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_ChiTietPhong] PRIMARY KEY CLUSTERED 
(
	[IdPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DanhGia]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhGia](
	[IdHoaDon] [nvarchar](450) NOT NULL,
	[IdNguoiDung] [nvarchar](450) NULL,
	[SoSao] [int] NULL,
	[NhanXet] [nvarchar](max) NULL,
 CONSTRAINT [PK_DanhGia] PRIMARY KEY CLUSTERED 
(
	[IdHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[IdHoaDon] [nvarchar](450) NOT NULL,
	[IdNguoiDung] [nvarchar](450) NULL,
	[IdPhong] [nvarchar](450) NULL,
	[SoTreEm] [int] NULL,
	[SoNguoiLon] [int] NULL,
	[GioCheckin] [datetime] NULL,
	[GioCheckout] [datetime] NULL,
	[PhuThu] [decimal](18, 2) NULL,
	[VAT] [decimal](18, 2) NULL,
	[TongTien] [decimal](18, 2) NULL,
	[TrangThai] [nvarchar](450) NULL,
	[YeuCau] [nvarchar](450) NULL,
	[TenPhong] [nvarchar](450) NULL,
	[UrlImages] [nvarchar](450) NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[IdHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiDung]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[IdNguoiDung] [nvarchar](450) NOT NULL,
	[IdQuyen] [nvarchar](450) NULL,
	[Ten] [nvarchar](100) NULL,
	[SoDienThoai] [nvarchar](15) NULL,
	[Email] [nvarchar](100) NULL,
	[MatKhau] [nvarchar](100) NULL,
	[GioiTinh] [nvarchar](10) NULL,
	[NgaySinh] [date] NULL,
	[AnhDaiDien] [nvarchar](max) NULL,
 CONSTRAINT [PK_NguoiDung] PRIMARY KEY CLUSTERED 
(
	[IdNguoiDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong](
	[Id] [nvarchar](450) NOT NULL,
	[UrlImage] [nvarchar](max) NULL,
	[TenPhong] [nvarchar](100) NULL,
	[SoLuong] [int] NULL,
	[GiaPhong] [decimal](18, 2) NULL,
	[GiaSauGiam] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Phong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongBao]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongBao](
	[TenThongBao] [varchar](255) NOT NULL,
	[Tttb] [int] NULL,
 CONSTRAINT [PK_ThongBao] PRIMARY KEY CLUSTERED 
(
	[TenThongBao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThongTinWeb]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThongTinWeb](
	[LoaiThongTin] [nvarchar](100) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[UrlImages] [nvarchar](max) NULL,
 CONSTRAINT [PK_ThongTinWeb] PRIMARY KEY CLUSTERED 
(
	[LoaiThongTin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TuVan]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TuVan](
	[Email] [nvarchar](100) NULL,
	[NgayGioNhan] [datetime] NULL,
	[IdTuVan] [nvarchar](450) NOT NULL,
	[Ten] [nvarchar](255) NULL,
	[LoiNhan] [nvarchar](max) NULL,
 CONSTRAINT [PK_TuVan] PRIMARY KEY CLUSTERED 
(
	[IdTuVan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[XacThuc]    Script Date: 29/01/2024 7:48:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XacThuc](
	[IdNguoiDung] [nvarchar](450) NOT NULL,
	[TrangThai] [int] NULL,
	[MaXacThuc] [nvarchar](50) NULL,
 CONSTRAINT [PK_XacThuc] PRIMARY KEY CLUSTERED 
(
	[IdNguoiDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231123143403_edit', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231125032221_edit', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231125041446_init', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231125042917_edit', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231128033414_init', N'7.0.14')
GO
INSERT [dbo].[Blog] ([IdBlog], [UrlImage], [MoTa], [Tieude]) VALUES (N'38ec927e-f5b4-4610-994b-ffff34472e11', N'[{"UrlImage":"\\images\\3915ac64-62af-4205-8637-cb7c362047d3-caption.jpg"}]', N'Hàng năm, chúng tôi trao giải cho các điểm đến, khách sạn, nhà hàng và hoạt động giải trí yêu thích của khách du lịch trên khắp thế giới, dựa trên đánh giá và xếp hạng được thu thập trong 12 tháng qua. Vì vậy, những địa điểm đạt Giải thưởng Best of the Best của Travellers'' Choice của chúng tôi do các bạn quyết định: những khách du lịch thực sự từ khắp nơi, chia sẻ ý kiến và câu chuyện thực tế.
Các địa điểm đạt giải năm 2023 là sự tưởng thưởng cho mọi điều chúng ta đã làm vào năm ngoái—tất cả những địa điểm chúng ta đã khám phá và tất cả những lần chúng ta hào hứng nói “có” với những chuyến phiêu lưu mới. Chúng tôi sẽ hé lộ những địa điểm đạt giải theo hạng mục trong suốt cả năm, vì vậy hãy liên tục kiểm tra lại để biết thông tin mới nhất.
Để tìm hiểu thêm về cách lựa chọn địa điểm đạt giải, hãy truy cập trang chính sách giải thưởng của chúng tôi.', N'Giải thưởng Best of the Best của Travellers'' Choice 2023')
INSERT [dbo].[Blog] ([IdBlog], [UrlImage], [MoTa], [Tieude]) VALUES (N'3912c810-249a-4b01-acd2-bb82c97c80dd', N'[{"UrlImage":"\\images\\no_image.jpg","Position":1}]', N'ccc cxc e', N'tesst3')
INSERT [dbo].[Blog] ([IdBlog], [UrlImage], [MoTa], [Tieude]) VALUES (N'4f8171e9-c7bf-4a57-9b72-7773168e49c4', N'[{"UrlImage":"\\images\\dde6295a-759a-489a-b0e1-ed467323ac3c-camnhi-230206040231-cho-dem-son-tra-1.jpg"}]', N'Chợ đêm Sơn Trà là một trong những điểm đến hấp dẫn nhất của thành phố Đà Nẵng. Địa điểm du lịch Đà Nẵng này không chỉ thu hút du khách bởi các mặt hàng thời trang, lưu niệm, mà còn bởi những món ăn đường phố ngon tuyệt.

Chợ đêm Sơn Trà được ví như một chiếc túi thần kì của Doraemon, bất cứ thứ gì khách du lịch Đà Nẵng cần, nơi đây đều có cả. Từ món ăn ngon, quần áo đẹp, quà lưu niệm xinh xắn... đến những trò chơi giải trí vui quên lối về', N'Thỏa mãn đam mê ẩm thực tại chợ đêm Sơn Trà Đà Nẵng')
INSERT [dbo].[Blog] ([IdBlog], [UrlImage], [MoTa], [Tieude]) VALUES (N'5646fe2f-9669-42af-a736-813dd4c67668', N'[{"UrlImage":"\\images\\79d8a94f-45fd-4ed2-bab3-9590425c0d42-thanh0310-235221025250-dia-diem-du-lich-quy-nhon-1.jpg"}]', N'Gây ấn tượng trong lòng du khách bởi thiên đường biển xinh đẹp cùng địa danh nổi tiếng để sống ảo và nghỉ dưỡng, Quy Nhơn luôn là điểm đến hàng đầu của nhiều du khách vào dịp Tết. Cùng “bỏ túi” những địa điểm du lịch Quy Nhơn Tết 2024 đáng trải nghiệm nhất dưới đây nhé.', N'5 địa điểm du lịch Quy Nhơn Tết đẹp mê hoặc lòng người')
INSERT [dbo].[Blog] ([IdBlog], [UrlImage], [MoTa], [Tieude]) VALUES (N'6f3a6f8b-d0cf-4ab0-b8de-c6f20970dadf', N'[{"UrlImage":"\\images\\a73cf1fc-7123-44c4-87ff-102046dfc97e-thanh0310-233820023833-tour-dem-ha-noi-01.png"}]', N'Hà Nội nghìn năm văn hiến luôn gây ấn tượng trong lòng du khách bởi vẻ đẹp truyền thống kết hợp với hiện đại. Du khách thường ấn tượng với vẻ đẹp Hà Nội vào ban ngày, nhưng ít ai biết được rằng, Hà Nội về đêm còn lung linh, huyền ảo và mang đậm giá trị lịch sử truyền thống. Nếu những ai ưa thích vẻ đẹp Thủ đô về đêm, hãy ghé thăm 4 tour đêm nên trải nghiệm khi đến Hà Nội.

Tour du lịch đêm Hà Nội mang đến cho du khách trải nghiệm đầy thú vị và sâu sắc về vẻ đẹp độc đáo của thủ đô lịch sử này khi về đêm. Với sự kết hợp hài hòa giữa sự hiện đại và nét cổ kính, Hà Nội tỏa sáng với những đặc điểm văn hóa, lịch sử và ẩm thực độc đáo.', N'Gây ấn tượng trong lòng du khách bởi thiên đường biển xinh đẹp')
INSERT [dbo].[Blog] ([IdBlog], [UrlImage], [MoTa], [Tieude]) VALUES (N'82b6e5fb-bc5a-4baf-865a-3eb1ce2af159', N'[{"UrlImage":"\\images\\a38d58e3-4ed3-4bf5-868f-4e46a27f598f-camnhi-233215113222-nhung-lang-det-tho-cam-tay-bac-1.jpg"}]', N'Tây Bắc là một vùng đất xinh đẹp, thơ mộng với những bản làng dân tộc thiểu số mang đậm bản sắc văn hóa. Một trong những nét văn hóa đặc sắc của vùng đất này chính là nghề dệt thổ cẩm. Những sản phẩm thổ cẩm của người dân Tây Bắc không chỉ đẹp mắt, tinh xảo mà còn mang đậm giá trị truyền thống.

Nếu có dịp ghé thăm Tây Bắc, bạn đừng quên ghé thăm những làng dệt thổ cẩm để tìm hiểu về nghề truyền thống này và mua những món đồ lưu niệm độc đáo về làm quà.', N'Thăm những làng dệt thổ cẩm Tây Bắc, mua đồ lưu niệm về làm quà')
INSERT [dbo].[Blog] ([IdBlog], [UrlImage], [MoTa], [Tieude]) VALUES (N'a18ade66-17cf-4544-ab2b-de0a15e2d6ac', N'[{"UrlImage":"\\images\\e648a8fa-2bf3-46b8-8b91-cb91d0800d3e-camnhi-232913052924-mua-hoa-dep-o-cao-bang-dong-bac.jpg"}]', N'Cao Bằng là một tỉnh miền núi thuộc vùng Đông Bắc Việt Nam, không chỉ nổi tiếng với những danh lam thắng cảnh hùng vĩ, những di tích lịch sử hào hùng và những món ăn đặc sản độc đáo, mà còn có những mùa hoa đẹp đến nao lòng khách du lịch. Mỗi mùa, du lịch Cao Bằng lại mang một vẻ đẹp riêng, thu hút du khách thập phương đến tham quan và chiêm ngưỡng.

Cao Bằng là một vùng đất có khí hậu ôn hòa, mát mẻ, thuận lợi cho sự phát triển của nhiều loài hoa. Cứ đến tháng 11, 12 hằng năm, dã quỳ lại đem theo sắc vàng sưởi ấm những ngày mưa phùn, gió bấc của mùa đông nơi rẻo cao Đông Bắc. 
 
Mỗi độ đông đi, xuân về, chắc hẳn ai cũng ngẩn ngơ, xao xuyến trước bức tranh thiên nhiên tươi đẹp của đào khoe sắc thắm, mận trắng tinh khôi. Mùa xuân cũng là mùa của các loài hoa nối tiếp nhau. Vào khoảng tháng 3, tháng 4, khắp chốn ngập tràn sắc màu hoa lê, hoa gạo, hoa trẩu, đỗ quyên… Có lẽ, thời điểm này chính là mùa hoa đẹp nhất trong năm ở Cao Bằng.', N'Mê mẩn trước những mùa hoa đẹp ở Cao Bằng, khoe sắc thắm giữa vùng cao Đông Bắc')
INSERT [dbo].[Blog] ([IdBlog], [UrlImage], [MoTa], [Tieude]) VALUES (N'fa9de926-f1e1-4b59-be7a-3eb1a86f9dab', N'[{"UrlImage":"\\images\\dc0ecafc-4171-47dd-9cbb-0c00417269b6-camnhi-233602053627-dia-diem-ngam-binh-minh-tay-bac.jpg"}]', N'Tây Bắc là một vùng đất với thiên nhiên hùng vĩ, thơ mộng, là điểm đến yêu thích của nhiều du khách trong và ngoài nước. Đặc biệt, vùng đất này còn sở hữu nhiều địa điểm ngắm bình minh đẹp mê đắm, khiến du khách phải say lòng.', N'Những địa điểm ngắm bình minh đẹp Tây Bắc mê hoặc du khách tứ phương')
GO
INSERT [dbo].[ChiTietPhong] ([IdPhong], [SoNguoiLon], [SoTreEm], [DienTich], [MoTa]) VALUES (N'00e5a85c-2a7e-44b0-a8b5-fca22ef544c1', 2, 1, 25, N'1')
INSERT [dbo].[ChiTietPhong] ([IdPhong], [SoNguoiLon], [SoTreEm], [DienTich], [MoTa]) VALUES (N'772b5087-0c15-425d-90ae-1211cd8543e7', 2, 1, 50, N'1')
INSERT [dbo].[ChiTietPhong] ([IdPhong], [SoNguoiLon], [SoTreEm], [DienTich], [MoTa]) VALUES (N'95390332-52ce-409a-a8ac-be481b3c5584', 2, 1, 50, N'1')
INSERT [dbo].[ChiTietPhong] ([IdPhong], [SoNguoiLon], [SoTreEm], [DienTich], [MoTa]) VALUES (N'ac88e240-127f-4ac3-b823-91568f065949', 2, 1, 30, N'1')
INSERT [dbo].[ChiTietPhong] ([IdPhong], [SoNguoiLon], [SoTreEm], [DienTich], [MoTa]) VALUES (N'c7b656c7-7399-4a3f-bd5d-4be160b0be0d', 2, 1, 25, N'1')
INSERT [dbo].[ChiTietPhong] ([IdPhong], [SoNguoiLon], [SoTreEm], [DienTich], [MoTa]) VALUES (N'c7cdeea9-df77-4dad-94c7-c48e63792843', 2, 1, 35, NULL)
INSERT [dbo].[ChiTietPhong] ([IdPhong], [SoNguoiLon], [SoTreEm], [DienTich], [MoTa]) VALUES (N'cd1e7f3e-2139-4e8c-b8b9-e9a07e9fdb0f', 2, 2, 22, N'mo ta phong')
GO
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'04fe0793-0ac5-4a12-ad95-13471760be9d', N'1e5ad1bb-7c9b-4572-ba4a-8ad1960c1cbc', N'95390332-52ce-409a-a8ac-be481b3c5584', 1, 2, CAST(N'2024-01-08T00:00:00.000' AS DateTime), CAST(N'2024-01-12T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(6912000.00 AS Decimal(18, 2)), N'Thành công', NULL, N'Premier Suite Room', N'\images\4edde15e-9fd3-4e86-a5c8-4e57dbcb7d3f-ronnie-george-m78oBvRHBm0-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'1044e84e-4dac-4904-8f0c-4fa167e0f31a', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-29T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(2592000.00 AS Decimal(18, 2)), N'Chờ nhận phòng', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'13e74b5c-7408-41f3-8339-35b7503ef7d3', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'00e5a85c-2a7e-44b0-a8b5-fca22ef544c1', 1, 2, CAST(N'2023-12-15T00:00:00.000' AS DateTime), CAST(N'2023-12-16T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(972000.00 AS Decimal(18, 2)), N'Thành công', NULL, N'Deluxe Room', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'20a86a6e-8abf-4555-9e2a-7d898c7d6428', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-27T00:00:00.000' AS DateTime), CAST(N'2024-02-29T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(864000.00 AS Decimal(18, 2)), N'Chờ nhận phòng', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'26fb6916-38ae-4841-8cbf-a6b58bb01a32', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'95390332-52ce-409a-a8ac-be481b3c5584', 1, 2, CAST(N'2023-12-09T00:00:00.000' AS DateTime), CAST(N'2023-12-10T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(1728000.00 AS Decimal(18, 2)), N'Thành công', N'aaaâ', N'Premier Suite Room', N'\images\42e8660b-5407-44d6-bac9-baf6f20bdf7f-roberto-nickson-emqnSQwQQDo-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'2f382556-3f3e-45a3-9755-08c78026f9b0', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-05T00:00:00.000' AS DateTime), CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(2160000.00 AS Decimal(18, 2)), N'Chờ nhận phòng', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'3ad32dff-d04d-4a13-9ade-bd0a9ff52ba6', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-15T00:00:00.000' AS DateTime), CAST(N'2024-02-25T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(4320000.00 AS Decimal(18, 2)), N'Chờ nhận phòng', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'3bfe45cb-564e-4665-b4d7-2548e1bf4d2b', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-07T00:00:00.000' AS DateTime), CAST(N'2024-02-08T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(432000.00 AS Decimal(18, 2)), N'Chờ nhận phòng', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'40d3fca6-f582-4de7-83ff-b6e1591c82c8', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-01T00:00:00.000' AS DateTime), CAST(N'2024-02-02T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(432000.00 AS Decimal(18, 2)), N'Đã hủy', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'449a2e27-528b-4db6-94fd-891af19ec791', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'772b5087-0c15-425d-90ae-1211cd8543e7', 1, 2, CAST(N'2023-12-10T00:00:00.000' AS DateTime), CAST(N'2023-12-19T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(14580000.00 AS Decimal(18, 2)), N'Đã hủy', NULL, N'Premier Suite Room', N'\images\f3b5dfa8-9881-4cf7-ad93-b292560a86e7-frames-for-your-heart-mdwOo5PeXpE-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'476c40b2-9e34-4730-ad41-26b73587d7c3', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'00e5a85c-2a7e-44b0-a8b5-fca22ef544c1', 1, 2, CAST(N'2023-12-09T00:00:00.000' AS DateTime), CAST(N'2023-12-10T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(972000.00 AS Decimal(18, 2)), N'Thành công', NULL, N'Deluxe Room', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'4fb0cd93-2dff-4c8c-a616-43e10e44ccca', N'12701611-8677-4627-af78-33a8a5acc9df', N'cd1e7f3e-2139-4e8c-b8b9-e9a07e9fdb0f', 2, 2, CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-02-20T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(1080000.00 AS Decimal(18, 2)), N'Chờ nhận phòng', N'adas cdsf', N'phong moi', N'\images\602595e4-901f-402a-a83c-dda281fd6104-3.png')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'519bfe29-f99a-4b2d-a447-e0afe35215b2', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(N'2024-02-26T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(1296000.00 AS Decimal(18, 2)), N'Đã hủy', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'52fc4f5b-a8af-40fe-9d54-ea4641e20b27', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'95390332-52ce-409a-a8ac-be481b3c5584', 1, 2, CAST(N'2023-12-09T00:00:00.000' AS DateTime), CAST(N'2023-12-10T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(1728000.00 AS Decimal(18, 2)), N'Đã hủy', NULL, N'Premier Suite Room', N'\images\42e8660b-5407-44d6-bac9-baf6f20bdf7f-roberto-nickson-emqnSQwQQDo-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'7c998858-ceb6-4dc5-8389-7c724d51613a', N'1e5ad1bb-7c9b-4572-ba4a-8ad1960c1cbc', N'00e5a85c-2a7e-44b0-a8b5-fca22ef544c1', 1, 2, CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-01-02T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(972000.00 AS Decimal(18, 2)), N'Thành công', N'oooooooooooooooooooo', N' Deluxe Room', N'\images\95982d91-3d54-4b4f-a107-06b82e4c57f2-mk-s-_-VFMl0TKlg-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'83a95976-286b-48ff-a525-7d78abbde388', N'1e5ad1bb-7c9b-4572-ba4a-8ad1960c1cbc', N'95390332-52ce-409a-a8ac-be481b3c5584', 1, 2, CAST(N'2024-01-15T00:00:00.000' AS DateTime), CAST(N'2024-01-20T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(8640000.00 AS Decimal(18, 2)), N'Thành công', N'dsfghj', N'Premier Suite Room', N'\images\4edde15e-9fd3-4e86-a5c8-4e57dbcb7d3f-ronnie-george-m78oBvRHBm0-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'99dbf913-4683-4319-9a1a-82edae5841f3', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-26T00:00:00.000' AS DateTime), CAST(N'2024-02-27T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(432000.00 AS Decimal(18, 2)), N'Đã hủy', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'9dff6ffb-2d60-454f-814a-9973836c07e9', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-02-20T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(4320000.00 AS Decimal(18, 2)), N'Chờ nhận phòng', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'c0d5e19a-866e-45e3-82ea-13f8bd56bdff', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'00e5a85c-2a7e-44b0-a8b5-fca22ef544c1', 1, 1, CAST(N'2023-12-07T00:00:00.000' AS DateTime), CAST(N'2023-12-09T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(1944000.00 AS Decimal(18, 2)), N'Đã hủy', NULL, N'Deluxe Room', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'cf89dcce-76b0-46ee-9af0-42cee0c5f696', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'95390332-52ce-409a-a8ac-be481b3c5584', 1, 2, CAST(N'2023-12-09T00:00:00.000' AS DateTime), CAST(N'2023-12-10T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(1728000.00 AS Decimal(18, 2)), N'Thành công', NULL, N'Premier Suite Room', N'\images\42e8660b-5407-44d6-bac9-baf6f20bdf7f-roberto-nickson-emqnSQwQQDo-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'd036c88b-91df-476f-8f89-3d3d79b4b5e4', N'1e5ad1bb-7c9b-4572-ba4a-8ad1960c1cbc', N'95390332-52ce-409a-a8ac-be481b3c5584', 1, 2, CAST(N'2024-01-18T00:00:00.000' AS DateTime), CAST(N'2024-01-25T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(12096000.00 AS Decimal(18, 2)), N'Đã hủy', NULL, N'Premier Suite Room', N'\images\4edde15e-9fd3-4e86-a5c8-4e57dbcb7d3f-ronnie-george-m78oBvRHBm0-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'd1da3c01-7020-44ef-ab8a-9e537861fa92', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'88cd6a0c-53be-4b39-943b-e04a9da4c1a4', 2, 2, CAST(N'2024-02-21T00:00:00.000' AS DateTime), CAST(N'2024-02-23T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(864000.00 AS Decimal(18, 2)), N'Đã hủy', NULL, N'test', N'\images\no_image.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'd83c86e0-342c-4fe6-853b-d756f81b1653', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'772b5087-0c15-425d-90ae-1211cd8543e7', 1, 1, CAST(N'2023-12-07T00:00:00.000' AS DateTime), CAST(N'2023-12-09T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(2.16 AS Decimal(18, 2)), N'Đã hủy', NULL, N'Premier Suite Room', N'\images\f3b5dfa8-9881-4cf7-ad93-b292560a86e7-frames-for-your-heart-mdwOo5PeXpE-unsplash.jpg')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'db441cf1-d0ae-4e97-9758-ce17437b9008', N'12701611-8677-4627-af78-33a8a5acc9df', N'cd1e7f3e-2139-4e8c-b8b9-e9a07e9fdb0f', 2, 2, CAST(N'2024-02-15T00:00:00.000' AS DateTime), CAST(N'2024-02-25T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(1080000.00 AS Decimal(18, 2)), N'Chờ nhận phòng', NULL, N'phong moi', N'\images\602595e4-901f-402a-a83c-dda281fd6104-3.png')
INSERT [dbo].[HoaDon] ([IdHoaDon], [IdNguoiDung], [IdPhong], [SoTreEm], [SoNguoiLon], [GioCheckin], [GioCheckout], [PhuThu], [VAT], [TongTien], [TrangThai], [YeuCau], [TenPhong], [UrlImages]) VALUES (N'e5baed48-5e9f-403c-9d7a-141887228d24', N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'00e5a85c-2a7e-44b0-a8b5-fca22ef544c1', 1, 1, CAST(N'2023-12-07T00:00:00.000' AS DateTime), CAST(N'2023-12-09T00:00:00.000' AS DateTime), CAST(0.00 AS Decimal(18, 2)), CAST(0.08 AS Decimal(18, 2)), CAST(1944000.00 AS Decimal(18, 2)), N'Thành công', NULL, N'Deluxe Room', N'\images\no_image.jpg')
GO
INSERT [dbo].[NguoiDung] ([IdNguoiDung], [IdQuyen], [Ten], [SoDienThoai], [Email], [MatKhau], [GioiTinh], [NgaySinh], [AnhDaiDien]) VALUES (N'12701611-8677-4627-af78-33a8a5acc9df', N'User', N'thinh', N'1232323', N'ga1073592@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', NULL, NULL, NULL)
INSERT [dbo].[NguoiDung] ([IdNguoiDung], [IdQuyen], [Ten], [SoDienThoai], [Email], [MatKhau], [GioiTinh], [NgaySinh], [AnhDaiDien]) VALUES (N'1401c386-77de-410a-b53c-5c6acedef044', N'User', N'KhaMinh', NULL, N'duongminh19055c@gmail.com', N'Ix580JrxnYy/Cwbidx1H0A==', NULL, NULL, NULL)
INSERT [dbo].[NguoiDung] ([IdNguoiDung], [IdQuyen], [Ten], [SoDienThoai], [Email], [MatKhau], [GioiTinh], [NgaySinh], [AnhDaiDien]) VALUES (N'1e5ad1bb-7c9b-4572-ba4a-8ad1960c1cbc', N'User', N'quoc thinh', N'0938672903', N'thinhle.31211025134@st.ueh.edu.vn', N'gdyb21LQTcIANtvYMT7QVQ==', N'nam', CAST(N'2003-03-29' AS Date), NULL)
INSERT [dbo].[NguoiDung] ([IdNguoiDung], [IdQuyen], [Ten], [SoDienThoai], [Email], [MatKhau], [GioiTinh], [NgaySinh], [AnhDaiDien]) VALUES (N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', N'User', N'thinh', N'344444', N'lequocthinh401@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', N'aaa', CAST(N'2003-03-29' AS Date), NULL)
INSERT [dbo].[NguoiDung] ([IdNguoiDung], [IdQuyen], [Ten], [SoDienThoai], [Email], [MatKhau], [GioiTinh], [NgaySinh], [AnhDaiDien]) VALUES (N'a0210667-bc66-4a7f-8c33-c689153081cf', N'User', N'nhan', NULL, N'nhanpham.31211021379@st.ueh.edu.vn', N'gdyb21LQTcIANtvYMT7QVQ==', NULL, NULL, NULL)
INSERT [dbo].[NguoiDung] ([IdNguoiDung], [IdQuyen], [Ten], [SoDienThoai], [Email], [MatKhau], [GioiTinh], [NgaySinh], [AnhDaiDien]) VALUES (N'd07ce7c7-666a-4a98-a2ff-85dcca77479c', N'Admin', N'admin', NULL, N'admin@gmail.com', N'gdyb21LQTcIANtvYMT7QVQ==', NULL, NULL, NULL)
GO
INSERT [dbo].[Phong] ([Id], [UrlImage], [TenPhong], [SoLuong], [GiaPhong], [GiaSauGiam]) VALUES (N'00e5a85c-2a7e-44b0-a8b5-fca22ef544c1', N'[{"UrlImage":"\\images\\984a5895-d887-47f4-9a93-b2afc8ce59d9-mk-s-_-VFMl0TKlg-unsplash.jpg","Position":1}]', N' Deluxe Room', 2, CAST(1000000.00 AS Decimal(18, 2)), CAST(900000.00 AS Decimal(18, 2)))
INSERT [dbo].[Phong] ([Id], [UrlImage], [TenPhong], [SoLuong], [GiaPhong], [GiaSauGiam]) VALUES (N'772b5087-0c15-425d-90ae-1211cd8543e7', N'[{"UrlImage":"\\images\\f3b5dfa8-9881-4cf7-ad93-b292560a86e7-frames-for-your-heart-mdwOo5PeXpE-unsplash.jpg"}]', N'Premier Suite Room', 1, CAST(1800000.00 AS Decimal(18, 2)), CAST(1500000.00 AS Decimal(18, 2)))
INSERT [dbo].[Phong] ([Id], [UrlImage], [TenPhong], [SoLuong], [GiaPhong], [GiaSauGiam]) VALUES (N'95390332-52ce-409a-a8ac-be481b3c5584', N'[{"UrlImage":"\\images\\4edde15e-9fd3-4e86-a5c8-4e57dbcb7d3f-ronnie-george-m78oBvRHBm0-unsplash.jpg"},{"UrlImage":"\\images\\42e8660b-5407-44d6-bac9-baf6f20bdf7f-roberto-nickson-emqnSQwQQDo-unsplash.jpg"}]', N'Premier Suite Room VIP', 2, CAST(2000000.00 AS Decimal(18, 2)), CAST(1600000.00 AS Decimal(18, 2)))
INSERT [dbo].[Phong] ([Id], [UrlImage], [TenPhong], [SoLuong], [GiaPhong], [GiaSauGiam]) VALUES (N'ac88e240-127f-4ac3-b823-91568f065949', N'[{"UrlImage":"\\images\\4e654181-d537-4602-9706-d6c5f20fd5cb-mk-s-vKyHBfBzEdE-unsplash.jpg"}]', N'Deluxe Room VIP', 1, CAST(1000000.00 AS Decimal(18, 2)), CAST(900000.00 AS Decimal(18, 2)))
INSERT [dbo].[Phong] ([Id], [UrlImage], [TenPhong], [SoLuong], [GiaPhong], [GiaSauGiam]) VALUES (N'c7b656c7-7399-4a3f-bd5d-4be160b0be0d', N'[{"UrlImage":"\\images\\85744c44-7bb9-4189-9b98-fb2aa66f7870-mk-s-OYpkvbnqvHg-unsplash.jpg"}]', N'Deluxe Suite Room', 1, CAST(800000.00 AS Decimal(18, 2)), CAST(800000.00 AS Decimal(18, 2)))
INSERT [dbo].[Phong] ([Id], [UrlImage], [TenPhong], [SoLuong], [GiaPhong], [GiaSauGiam]) VALUES (N'c7cdeea9-df77-4dad-94c7-c48e63792843', N'[{"UrlImage":"\\images\\a013e7a1-ea8e-4d0e-b4e9-2b45b128507e-michael-oxendine-oD3-adGSEpE-unsplash.jpg"}]', N'Deluxe Suite Room VIP', 1, CAST(1000000.00 AS Decimal(18, 2)), CAST(1000000.00 AS Decimal(18, 2)))
INSERT [dbo].[Phong] ([Id], [UrlImage], [TenPhong], [SoLuong], [GiaPhong], [GiaSauGiam]) VALUES (N'cd1e7f3e-2139-4e8c-b8b9-e9a07e9fdb0f', N'[{"UrlImage":"\\images\\602595e4-901f-402a-a83c-dda281fd6104-3.png","Position":1},{"UrlImage":"\\images\\4ae9e1f3-6e5a-43c0-962a-772e44771efc-4.jpg","Position":1}]', N'phong moi', 2, CAST(100000.00 AS Decimal(18, 2)), CAST(100000.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[ThongBao] ([TenThongBao], [Tttb]) VALUES (N'email', 0)
INSERT [dbo].[ThongBao] ([TenThongBao], [Tttb]) VALUES (N'hoadon', 0)
GO
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Địa Chỉ', N'279 Nguyen Tri Phuong

', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Dịch Vụ Nhà Hàng', N'MerPerle Resorts & Hotels là chuỗi resort, khách sạn, nhà hàng sang trọng tại các thành phố du lịch của Việt Nam. Tại MerPerle, các dịch vụ đẳng cấp, chuẩn mực được chúng tôi trau chuốt với sự tận tuỵ và chuyên nghiệp với mong muốn tạo ra hành trình trải nghiệm tuyệt vời nhất đến quý khách khi lựa chọn kỳ nghỉ tại các điểm đến của chúng tôi. Rất mong được đón tiếp và phục vụ quý khách!', N'\images\9ba14524-6103-4acb-a851-91d8361d9bde-kellie-enge-P9CnUY-oTI8-unsplash.jpg')
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Du Lịch & Cắm Trại', N'MerPerle Resorts & Hotels là chuỗi resort, khách sạn, nhà hàng sang trọng tại các thành phố du lịch của Việt Nam. Tại MerPerle, các dịch vụ đẳng cấp, chuẩn mực được chúng tôi trau chuốt với sự tận tuỵ và chuyên nghiệp với mong muốn tạo ra hành trình trải nghiệm tuyệt vời nhất đến quý khách khi lựa chọn kỳ nghỉ tại các điểm đến của chúng tôi. Rất mong được đón tiếp và phục vụ quý khách!', N'\images\4cd34562-2902-4f94-8dc1-e3cf34290f6d-kellie-enge-5Zdr-pgDVHc-unsplash.jpg')
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Email', N'lequocthinh401@gmail.com', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Giới thiệu', N'MerPerle Resorts & Hotels là chuỗi resort, khách sạn, nhà hàng sang trọng tại các thành phố du lịch của Việt Nam. Tại MerPerle, các dịch vụ đẳng cấp, chuẩn mực được chúng tôi trau chuốt với sự tận tuỵ và chuyên nghiệp với mong muốn tạo ra hành trình trải nghiệm tuyệt vời nhất đến quý khách khi lựa chọn kỳ nghỉ tại các điểm đến của chúng tôi. Rất mong được đón tiếp và phục vụ quý khách!', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Link Địa Chỉ', N'https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3919.654956304175!2d106.6657830748696!3d10.761053189386807!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752ee4595019ad%3A0xf2a1b15c6af2c1a6!2zxJDhuqFpIGjhu41jIEtpbmggdOG6vyBUUC4gSOG7kyBDaMOtIE1pbmggKEPGoSBz4bufIEIp!5e0!3m2!1svi!2s!4v1701528650977!5m2!1svi!2s', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Link Facebook', N'https://www.facebook.com/profile.php?id=100082031046548', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Link Instagram', N'https://www.facebook.com/profile.php?id=100082031046548', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Link Linkedin', N'https://www.facebook.com/profile.php?id=100082031046548', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Link Twitter', N'https://www.facebook.com/profile.php?id=100082031046548', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Link YouTube', N'https://www.youtube.com/channel/UCydC1x6LyeEIHSl-6_fs0OA', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'SĐT', N'0938672903', NULL)
INSERT [dbo].[ThongTinWeb] ([LoaiThongTin], [MoTa], [UrlImages]) VALUES (N'Sự Kiện & Tổ Chức Tiệc', N'Chúng tôi đam mê du lịch. Mỗi ngày, chúng tôi truyền cảm hứng và tiếp cận hàng triệu khách du lịch trên 90 trang web địa phương bằng 41 ngôn ngữ. Vì vậy, khi cần đặt phòng khách sạn, nhà nghỉ cho thuê, khu nghỉ dưỡng, căn hộ, nhà khách hoặc nhà trên cây hoàn hảo, chúng tôi sẽ hỗ trợ bạn.', N'\images\20eb8e30-f643-4f23-8998-89a8a3175bc2-michael-oxendine-oD3-adGSEpE-unsplash.jpg')
GO
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2023-12-10T15:17:38.557' AS DateTime), N'05f84d15-3f7b-4afe-8c01-0e419fde46f7', N'Thinh', N'123')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2023-12-08T09:36:13.627' AS DateTime), N'0737d8fe-d12b-4397-b20e-fee3904d5c69', N'Thinh', N'dfsvdsv')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2023-12-10T14:52:43.677' AS DateTime), N'1ddb8533-026d-410e-8fce-7a18fc379157', N'Thinh', N'aaaa')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'admin@gmail.com', CAST(N'2023-12-07T02:25:51.310' AS DateTime), N'23aa3aa2-1377-4c75-b718-72df44cddc78', N'Thinh', N'sssss')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2023-12-07T01:29:18.040' AS DateTime), N'2ac22975-ce25-4ea2-94e7-a3a64204370a', N'Thinh', N'aaaaa')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2023-12-10T15:58:12.530' AS DateTime), N'2b852cdf-e790-4b4a-a486-818e7c590641', N'Thinh', N'ndnd ddd')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2024-01-28T23:45:08.863' AS DateTime), N'353327e7-4362-428b-a45d-0605e3dbadf7', N'Thinh', N'dgf svd')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2024-01-28T23:44:58.813' AS DateTime), N'406bbe60-36e6-45de-9226-90b069dc30ce', N'Thinh', N'a b c d')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2024-01-29T15:33:55.837' AS DateTime), N'44d3be5f-0eb5-4760-99d5-9708d7a1477b', N'Thinh', N'aaa   dâ')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2023-12-07T01:26:25.137' AS DateTime), N'4c37fdee-8e2b-485b-836b-8c544fac81eb', N'Thinh', N'aaaaaaa')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2023-12-10T15:58:07.207' AS DateTime), N'6d90f874-6623-4ca9-95a0-1d1e61cc6ce3', N'Thinh', N'aa ddddd')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'admin@gmail.com', CAST(N'2023-12-09T23:39:09.467' AS DateTime), N'6d9e2497-1736-4680-aafe-e2fbad2527a4', N'thinhthinh', N'adad')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2024-01-29T15:23:19.460' AS DateTime), N'8927e8d0-a82d-4dc8-9b6c-2d3e4332b929', N'Thinh', N'mm')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2023-12-07T01:24:21.873' AS DateTime), N'9d4703af-7419-478e-82bc-560e822e87e8', N'Thinh', N'aaaaaa')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2024-01-29T19:43:17.127' AS DateTime), N'a048cd6d-5f9a-4107-a1de-76027f8a0b0e', N'Thinh', N'aaaaa  bbb')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'abc@gmail.com', CAST(N'2023-12-07T23:03:14.013' AS DateTime), N'a10bb655-8d2a-4143-a754-b56125b1c5c9', N'Thinh', N'njhgf')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'abc@gmail.com', CAST(N'2023-12-07T15:23:00.060' AS DateTime), N'aac8db44-3cbf-49f8-b411-8f94c3e3dcca', N'Thinh', N'ttttt')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'abc@gmail.com', CAST(N'2023-12-07T02:26:21.547' AS DateTime), N'bafcb6e4-8a9e-4eba-b2f3-fab74b42b01d', N'Thinh', N'ggggg')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2024-01-26T19:39:21.857' AS DateTime), N'cac0a451-c29c-484b-9b5d-2ca817d7e16b', N'Thinh', N'assasa axasdsda')
INSERT [dbo].[TuVan] ([Email], [NgayGioNhan], [IdTuVan], [Ten], [LoiNhan]) VALUES (N'lequocthinh401@gmail.com', CAST(N'2024-01-29T15:33:50.913' AS DateTime), N'cbbf4cc6-7aef-415c-8b58-5bfff54dbec9', N'Thinh', N'aaa   dâ')
GO
INSERT [dbo].[XacThuc] ([IdNguoiDung], [TrangThai], [MaXacThuc]) VALUES (N'12701611-8677-4627-af78-33a8a5acc9df', 1, N'427172')
INSERT [dbo].[XacThuc] ([IdNguoiDung], [TrangThai], [MaXacThuc]) VALUES (N'1401c386-77de-410a-b53c-5c6acedef044', 1, N'820027')
INSERT [dbo].[XacThuc] ([IdNguoiDung], [TrangThai], [MaXacThuc]) VALUES (N'1e5ad1bb-7c9b-4572-ba4a-8ad1960c1cbc', 1, N'895535')
INSERT [dbo].[XacThuc] ([IdNguoiDung], [TrangThai], [MaXacThuc]) VALUES (N'9bbfe40a-cbd8-4b6a-98a9-f0661d72a8e2', 1, N'693137')
INSERT [dbo].[XacThuc] ([IdNguoiDung], [TrangThai], [MaXacThuc]) VALUES (N'a0210667-bc66-4a7f-8c33-c689153081cf', 0, N'485680')
INSERT [dbo].[XacThuc] ([IdNguoiDung], [TrangThai], [MaXacThuc]) VALUES (N'b932eddf-15e6-40d0-9a72-7a12a8fa36e2', 2, N'820336')
INSERT [dbo].[XacThuc] ([IdNguoiDung], [TrangThai], [MaXacThuc]) VALUES (N'bff558d7-78e5-44af-8114-0daaf5d25cfe', 1, N'742560')
INSERT [dbo].[XacThuc] ([IdNguoiDung], [TrangThai], [MaXacThuc]) VALUES (N'd07ce7c7-666a-4a98-a2ff-85dcca77479c', 1, NULL)
GO
ALTER TABLE [dbo].[TuVan] ADD  DEFAULT (N'') FOR [IdTuVan]
GO
USE [master]
GO
ALTER DATABASE [dbQuanLyKhachSan] SET  READ_WRITE 
GO
