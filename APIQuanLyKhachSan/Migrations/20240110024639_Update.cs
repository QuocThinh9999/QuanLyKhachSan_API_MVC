using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIQuanLyKhachSan.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhong_Phong",
                table: "ChiTietPhong");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_HoaDon",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGia_NguoiDung",
                table: "DanhGia");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_NguoiDung",
                table: "HoaDon");

            migrationBuilder.DropForeignKey(
                name: "FK_HoaDon_Phong",
                table: "HoaDon");

            migrationBuilder.DropIndex(
                name: "IX_HoaDon_IdNguoiDung",
                table: "HoaDon");

            migrationBuilder.DropIndex(
                name: "IX_HoaDon_IdPhong",
                table: "HoaDon");

            migrationBuilder.DropIndex(
                name: "IX_DanhGia_IdNguoiDung",
                table: "DanhGia");

            migrationBuilder.DropColumn(
                name: "ConTrong",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "ThoiGianKhuyenMai",
                table: "Phong");

            migrationBuilder.AlterColumn<string>(
                name: "IdTuVan",
                table: "TuVan",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "(N'')",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "LoiNhan",
                table: "TuVan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ten",
                table: "TuVan",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlImages",
                table: "ThongTinWeb",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdLoaiPhong",
                table: "Phong",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VAT",
                table: "HoaDon",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "TenPhong",
                table: "HoaDon",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PhuThu",
                table: "HoaDon",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "YeuCau",
                table: "HoaDon",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SoTreEm",
                table: "ChiTietPhong",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SoNguoiLon",
                table: "ChiTietPhong",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DienTich",
                table: "ChiTietPhong",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    IdBlog = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UrlImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tieude = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.IdBlog);
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    TenThongBao = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Tttb = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBao", x => x.TenThongBao);
                });

            migrationBuilder.CreateTable(
                name: "XacThuc",
                columns: table => new
                {
                    IdNguoiDung = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    MaXacThuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XacThuc", x => x.IdNguoiDung);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "XacThuc");

            migrationBuilder.DropColumn(
                name: "LoiNhan",
                table: "TuVan");

            migrationBuilder.DropColumn(
                name: "Ten",
                table: "TuVan");

            migrationBuilder.DropColumn(
                name: "UrlImages",
                table: "ThongTinWeb");

            migrationBuilder.DropColumn(
                name: "IdLoaiPhong",
                table: "Phong");

            migrationBuilder.DropColumn(
                name: "YeuCau",
                table: "HoaDon");

            migrationBuilder.AlterColumn<string>(
                name: "IdTuVan",
                table: "TuVan",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValueSql: "(N'')");

            migrationBuilder.AddColumn<int>(
                name: "ConTrong",
                table: "Phong",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThoiGianKhuyenMai",
                table: "Phong",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VAT",
                table: "HoaDon",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenPhong",
                table: "HoaDon",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PhuThu",
                table: "HoaDon",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SoTreEm",
                table: "ChiTietPhong",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SoNguoiLon",
                table: "ChiTietPhong",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DienTich",
                table: "ChiTietPhong",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdNguoiDung",
                table: "HoaDon",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdPhong",
                table: "HoaDon",
                column: "IdPhong");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdNguoiDung",
                table: "DanhGia",
                column: "IdNguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhong_Phong",
                table: "ChiTietPhong",
                column: "IdPhong",
                principalTable: "Phong",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_HoaDon",
                table: "DanhGia",
                column: "IdHoaDon",
                principalTable: "HoaDon",
                principalColumn: "IdHoaDon");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGia_NguoiDung",
                table: "DanhGia",
                column: "IdNguoiDung",
                principalTable: "NguoiDung",
                principalColumn: "IdNguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_NguoiDung",
                table: "HoaDon",
                column: "IdNguoiDung",
                principalTable: "NguoiDung",
                principalColumn: "IdNguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_HoaDon_Phong",
                table: "HoaDon",
                column: "IdPhong",
                principalTable: "Phong",
                principalColumn: "Id");
        }
    }
}
