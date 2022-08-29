using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using Tiber_Sabun_Web.Models;

namespace Tiber_Sabun_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context VeriTabani = new Context();
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        //* Aldığı ID değerine göre (UrunTablosundaki ID) Kategori adı içeren ÜrünTablosu sorgu sonucu gönderecek.
        //  ID null gönderilirse Tüm tabloyu gönderecek.
        public IEnumerable KategoriliUrunListesi(int? id, string? Son6, int? Kategori)
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
                sorgu = sorgu.Where(x => x.ID == id).ToList();
            }
            else if (Kategori != null)
            {
                sorgu = sorgu.Where(x => x.KategoriID == Kategori).ToList();
            }
            else if (Son6 == ";")
            {
                if (sorgu.Count<4)
                {
                    sorgu = sorgu.OrderByDescending(x => x.KayitTarihi).ToList();
                }
                else
                {
                    sorgu = sorgu.OrderByDescending(x => x.KayitTarihi).Take(4).ToList();
                }
            }
            else if (!string.IsNullOrEmpty(Son6) && Son6 != ";")
            { 
                sorgu=sorgu.Where(x=>x.Adi.Contains(Son6.ToUpper())).ToList();
            }

            return sorgu;
        }
        //********************************************
        public IActionResult Index()
        {
            return View(KategoriliUrunListesi(null, ";",null));
        }

        public IActionResult UrunListe(int? id,string? ara)
        {
            if (id.HasValue)
            {
                return View(KategoriliUrunListesi(null, null,id));
            }
            if (!string.IsNullOrWhiteSpace(ara))
            {
                return View(KategoriliUrunListesi(null, ara,null));
            }else
            return View(KategoriliUrunListesi(null, null,null));
        }
        [HttpGet]
        public IActionResult UrunDetay(int? id)
        {
            ViewBag.Kategori = VeriTabani.UrunKategoriTablosu.Find(VeriTabani.UrunlerTablosu.Find(id).KategoriID.Value).Adi;
            return View(VeriTabani.UrunlerTablosu.Find(id));
        }

        public IActionResult Hakkinda()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}