using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Tiber_Sabun_Web.Models
{
    [Table ("UrunKategoriTablosu")]
    public class UrunKategoriTbl
    {
        [Key] public int KategoriID { get; set; }
        public string? Adi { get; set; }
        public string? Aciklama { get; set; }
        public DateTime KayitTarihi { get; set; }

    }
}
