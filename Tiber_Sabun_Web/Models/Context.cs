using Microsoft.EntityFrameworkCore;

namespace Tiber_Sabun_Web.Models
{
    public class Context :DbContext
    {
        public DbSet<UrunKategoriTbl> UrunKategoriTablosu { get; set; }
        public DbSet<UrunlerTbl> UrunlerTablosu { get; set; }
        public DbSet<GelenSoruTbl> GelenSoruTablosu { get; set; }
        public DbSet<SikcaSorulanSorularTbl> SikcaSorulanSorularTablosu { get; set; }
        public DbSet<KargoTbl> KargoTablosu { get; set; }
        public DbSet<YorumlarTbl> YorumTablosu { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connectionstring
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=TiberSabunWebVt;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
