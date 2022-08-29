using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace Tiber_Sabun_Web.Models
{
    [Table("GelenSoruTablosu")]
    public class GelenSoruTbl
    {
        [Key] public int ID { get; set; }
        public string? AdSoyad { get; set; }
        public string? Email { get; set; }
        public string? Mesaj { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}