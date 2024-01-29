using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIQuanLyKhachSan.Models;

[Table("ThongBao")]
public partial class ThongBao
{
    [Key]
    [StringLength(255)]
    [Unicode(false)]
    public string TenThongBao { get; set; } = null!;

    public int? Tttb { get; set; }
}
