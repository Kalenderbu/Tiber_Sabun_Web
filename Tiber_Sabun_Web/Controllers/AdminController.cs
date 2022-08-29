using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.ComponentModel;
using System.IO;
using Tiber_Sabun_Web.Models;

namespace Tiber_Sabun_Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly Context VeriTabani = new Context();
        private readonly IWebHostEnvironment _hostEnvironment;
        public AdminController(ILogger<AdminController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult AdminIndex()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminKategoriListe()
        {
            var Liste = VeriTabani.UrunKategoriTablosu.ToList();
            return View(Liste);
        }

        [HttpGet]
        public ActionResult AdminKategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminKategoriEkle([Bind("KategoriID", "Adi", "Aciklama", "KayitTarihi")] UrunKategoriTbl KategoriKayit)
        {
            if (KategoriKayit == null)
            {
                TempData["KayitOnay"] = "Kayıt hatası!";
                return RedirectToAction("AdminKategoriEkle", "Admin");
            }
            else
                KategoriKayit.KayitTarihi = DateTime.Now;
            VeriTabani.UrunKategoriTablosu.Add(KategoriKayit);
            await VeriTabani.SaveChangesAsync();
            TempData["KayitOnay"] = "Kategori eklendi!";
            return RedirectToAction("AdminKategoriListe", "Admin");
        }
        [HttpGet]
        public IActionResult AdminKategoriDuzelt(int? id)
        {
            UrunKategoriTbl KategoriNesne = new UrunKategoriTbl();
            if (id.HasValue)
            {
                KategoriNesne = VeriTabani.UrunKategoriTablosu.Find(id);
            }
            return View(KategoriNesne);
        }

        [HttpPost]
        public async Task<IActionResult> AdminKategoriDuzelt(
            [Bind("KategoriID", "Adi", "Aciklama", "KayitTarihi")] UrunKategoriTbl KategoriDuzelt)
        {
            UrunKategoriTbl DuzeltilecekNesne = VeriTabani.UrunKategoriTablosu.Find(KategoriDuzelt.KategoriID);
            DuzeltilecekNesne.Adi = KategoriDuzelt.Adi;
            DuzeltilecekNesne.Aciklama = KategoriDuzelt.Aciklama;
            DuzeltilecekNesne.KayitTarihi = DateTime.Now;
            VeriTabani.UrunKategoriTablosu.Update(DuzeltilecekNesne);
            await VeriTabani.SaveChangesAsync();
            TempData["KayitOnay"] = "Kategori güncellendi!";
            return RedirectToAction("AdminKategoriListe", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> KategoriSil(int? id)
        {
            UrunKategoriTbl KategoriSil = VeriTabani.UrunKategoriTablosu.Find(id);
            VeriTabani.UrunKategoriTablosu.Remove(KategoriSil);
            await VeriTabani.SaveChangesAsync();
            TempData["SilmeUyari"] = "Kategori silindi!";
            return RedirectToAction("AdminKategoriListe", "Admin");
        }

        [HttpGet]
        public ActionResult AdminUrunEkle()
        {
            var KategoriListe = VeriTabani.UrunKategoriTablosu.ToList();
            return View(KategoriListe);
        }


        [HttpPost]
        public async Task<IActionResult> AdminUrunEkle([Bind("ID", "KategoriID", "StokAdeti", "Adi", "Aciklama", "KullanilanYaglar", "Fiyat", "VideoLink", "Fotolar", "KayitTarihi", "Dosya", "Fotolar")] UrunlerTbl UrunKayit)
        {
            if (UrunKayit == null)
            {
                TempData["KayitOnay"] = "Ürün kayıt hatası!";
                return RedirectToAction("AdminUrunEkle", "Admin");
            }
            else
            {
                foreach (var f in UrunKayit.Dosya)
                {
                    var Uzanti = Path.GetExtension(f.FileName);
                    var YeniFotoAdi = Guid.NewGuid() + Uzanti;
                    var DosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UrunResimleri/", YeniFotoAdi);
                    var DosyaKayit = new FileStream(DosyaYolu, FileMode.Create);
                    await f.CopyToAsync(DosyaKayit);
                    UrunKayit.Fotolar += YeniFotoAdi + ";";
                }
                UrunKayit.Adi=UrunKayit.Adi.ToUpper();
                UrunKayit.Fiyat= double.Parse(UrunKayit.Fiyat.ToString().Replace(".", ","));
                UrunKayit.KayitTarihi = DateTime.Now;
                VeriTabani.UrunlerTablosu.Add(UrunKayit);
                await VeriTabani.SaveChangesAsync();
                TempData["KayitOnay"] = "Ürün eklendi!";
                return RedirectToAction("AdminUrunListe", "Admin");
            }
        }

        //* Aldığı ID değerine göre (UrunTablosundaki ID) Kategori adı içeren ÜrünTablosu sorgu sonucu gönderecek.
        //  ID null gönderilirse Tüm tabloyu gönderecek.
        public IList KategoriliUrunListesi(int? id)
        {
            var sorgu = (from tUrunler in VeriTabani.UrunlerTablosu
                         join tKategoriler in VeriTabani.UrunKategoriTablosu on tUrunler.KategoriID equals tKategoriler.KategoriID
                         select new
                         {
                             tUrunler.ID,
                             tUrunler.KategoriID,
                             tUrunler.Adi,
                             tUrunler.Aciklama,
                             tUrunler.Fiyat,
                             tUrunler.Dosya,
                             tUrunler.KayitTarihi,
                             tUrunler.Fotolar,
                             tUrunler.GecenSure,
                             tUrunler.KullanilanYaglar,
                             tUrunler.StokAdeti,
                             tUrunler.VideoLink,
                             KategoriAdi = tKategoriler.Adi,
                         }).ToList();
            if (id != null)
            {
                sorgu.Find(x => x.ID == id);
            }
            return sorgu;
        }
        //********************************************
        [HttpGet]
        public ActionResult AdminUrunListe()
        {
            return View(KategoriliUrunListesi(null));
        }

        [HttpGet]
        public IActionResult AdminUrunDuzelt(int? id)
        {
            ViewBag.Kategori = VeriTabani.UrunKategoriTablosu.Find(VeriTabani.UrunlerTablosu.Find(id).KategoriID.Value).Adi;
            return View(VeriTabani.UrunlerTablosu.Find(id));
        }

        [HttpPost]
        public async Task<IActionResult> AdminUrunDuzelt([Bind("ID", "KategoriID", "StokAdeti", "Adi", "Aciklama", "KullanilanYaglar", "Fiyat", "VideoLink", "Fotolar", "KayitTarihi", "Dosya", "Fotolar")] UrunlerTbl UrunDuzelt)
        {

            UrunlerTbl DuzeltilecekNesne = VeriTabani.UrunlerTablosu.Find(UrunDuzelt.ID);
            DuzeltilecekNesne.Adi = UrunDuzelt.Adi;
            DuzeltilecekNesne.KategoriID = UrunDuzelt.KategoriID;
            DuzeltilecekNesne.StokAdeti = UrunDuzelt.StokAdeti;
            DuzeltilecekNesne.KullanilanYaglar = UrunDuzelt.KullanilanYaglar;
            DuzeltilecekNesne.Aciklama = UrunDuzelt.Aciklama;
            DuzeltilecekNesne.Fiyat= double.Parse(UrunDuzelt.Fiyat.ToString().Replace(".", ","));
            DuzeltilecekNesne.VideoLink = UrunDuzelt.VideoLink;
            DuzeltilecekNesne.KayitTarihi = DateTime.Now;
            if (UrunDuzelt.Dosya != null || !string.IsNullOrWhiteSpace(UrunDuzelt.Fotolar))
            {
                string[] ResimListe = DuzeltilecekNesne.Fotolar.Split(';');
                for (int i = 1; i <= (ResimListe.Count() - 1); i++)
                {
                    var DosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UrunResimleri/");
                    var TamDosyaAdi = Path.Combine(DosyaYolu, @ResimListe[i - 1]);
                    System.IO.File.Delete(TamDosyaAdi);
                    DuzeltilecekNesne.Fotolar = null;
                }
                foreach (var f in UrunDuzelt.Dosya)
                {
                    var Uzanti = Path.GetExtension(f.FileName);
                    var YeniFotoAdi = Guid.NewGuid() + Uzanti;
                    var DosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UrunResimleri/", YeniFotoAdi);
                    var DosyaKayit = new FileStream(DosyaYolu, FileMode.Create);
                    await f.CopyToAsync(DosyaKayit);
                    DuzeltilecekNesne.Fotolar += YeniFotoAdi + ";";
                }
            }

            VeriTabani.UrunlerTablosu.Update(DuzeltilecekNesne);
            await VeriTabani.SaveChangesAsync();
            TempData["KayitOnay"] = "Ürün güncellendi!";
            return RedirectToAction("AdminUrunListe", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> UrunSil(int? id)
        {
            UrunlerTbl SilinecekUrun = VeriTabani.UrunlerTablosu.Find(id);
            // ****Fotoğraf Silme İşlemi
            if (!string.IsNullOrWhiteSpace(SilinecekUrun.Fotolar))
            {
                string[] ResimListe = SilinecekUrun.Fotolar.Split(';');
                for (int i = 1; i <= (ResimListe.Count() - 1); i++)
                {
                    var DosyaYolu = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UrunResimleri/");
                    var TamDosyaAdi = Path.Combine(DosyaYolu, @ResimListe[i - 1]);
                    if (System.IO.File.Exists(TamDosyaAdi))
                    {
                        System.IO.File.Delete(TamDosyaAdi);
                    }
                }
            }
            // Fotoğraf Silme İşlemi****
            VeriTabani.UrunlerTablosu.Remove(SilinecekUrun);
            await VeriTabani.SaveChangesAsync();
            TempData["SilmeUyari"] = "Ürün silindi!";
            return RedirectToAction("AdminUrunListe", "Admin");
        }
    }
}
