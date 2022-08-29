using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Tiber_Sabun_Web.Models
{
    [Table("KargoTablosu")]
    public class KargoTbl
    {
        [Key] public int KargoID { get; set; }
        public int UrunID { get; set; }
        public string? KargoAdi { get; set; }
        public string? Aciklama { get; set; }
        public double? KargoUcreti { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}
