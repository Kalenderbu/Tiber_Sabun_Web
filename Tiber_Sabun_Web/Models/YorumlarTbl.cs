using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiber_Sabun_Web.Models
{
     [Table("YorumlarTablosu")]
    public class YorumlarTbl
    {
        [Key]
        public int ID { get; set; }

        public int UrunID { get; set; }

        public string? AdSoyad { get; set; }
        public string? Baslik { get; set; }
        public string? Aciklama { get; set; }

        public int Puan { get; set; }

        public DateTime KayitTarihi { get; set; }
    }
}
