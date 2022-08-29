using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tiber_Sabun_Web.Migrations
{
    public partial class TiberWebInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GelenSoruTablosu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mesaj = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GelenSoruTablosu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KargoTablosu",
                columns: table => new
                {
                    KargoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    KargoAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KargoUcreti = table.Column<double>(type: "float", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KargoTablosu", x => x.KargoID);
                });

            migrationBuilder.CreateTable(
                name: "SikcaSorulanSorularTablosu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sira = table.Column<int>(type: "int", nullable: true),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SikcaSorulanSorularTablosu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UrunKategoriTablosu",
                columns: table => new
                {
                    KategoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunKategoriTablosu", x => x.KategoriID);
                });

            migrationBuilder.CreateTable(
                name: "UrunlerTablosu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriID = table.Column<int>(type: "int", nullable: true),
                    YorumID = table.Column<int>(type: "int", nullable: true),
                    KargoID = table.Column<int>(type: "int", nullable: true),
                    StokAdeti = table.Column<int>(type: "int", nullable: true),
                    Adi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullanilanYaglar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fiyat = table.Column<double>(type: "float", nullable: true),
                    VideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fotolar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunlerTablosu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "YorumlarTablosu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puan = table.Column<int>(type: "int", nullable: false),
                    KayitTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YorumlarTablosu", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GelenSoruTablosu");

            migrationBuilder.DropTable(
                name: "KargoTablosu");

            migrationBuilder.DropTable(
                name: "SikcaSorulanSorularTablosu");

            migrationBuilder.DropTable(
                name: "UrunKategoriTablosu");

            migrationBuilder.DropTable(
                name: "UrunlerTablosu");

            migrationBuilder.DropTable(
                name: "YorumlarTablosu");
        }
    }
}
