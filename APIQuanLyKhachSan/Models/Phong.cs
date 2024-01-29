﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APIQuanLyKhachSan.Models;

[Table("Phong")]
public partial class Phong
{
    [Key]
    public string Id { get; set; } = null!;

    public string? UrlImage { get; set; }

 

    [StringLength(100)]
    public string? TenPhong { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal GiaPhong { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal GiaSauGiam { get; set; }
}
