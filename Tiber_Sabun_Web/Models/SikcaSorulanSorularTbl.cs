using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Tiber_Sabun_Web.Models
{
    [Table ("SikcaSorulanSorularTablosu")]
    public class SikcaSorulanSorularTbl
    {
        [Key] public int ID { get; set; }
        public int? Sira { get; set; }
        public string? Baslik { get; set; }
        public string? Aciklama { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
