using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Tiber_Sabun_Web.Models
{
    [Table("UrunlerTablosu")]
    public class UrunlerTbl
    {
        [Key] public int ID { get; set; }
        public int? KategoriID { get; set; }
        public int? YorumID { get; set; }
        public int? KargoID { get; set; }
        public int? StokAdeti { get; set; }
        public string? Adi { get; set; }
        public string? Aciklama { get; set; }

        public string? KullanilanYaglar { get; set; }

        public double? Fiyat { get; set; }

        public string? VideoLink { get; set; }

        [NotMapped] public string? GecenSure { get; set; }

        [NotMapped] public ICollection<IFormFile>? Dosya { get; set; }     //Veri tabanına yansımayacak!
        public string? Fotolar { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}