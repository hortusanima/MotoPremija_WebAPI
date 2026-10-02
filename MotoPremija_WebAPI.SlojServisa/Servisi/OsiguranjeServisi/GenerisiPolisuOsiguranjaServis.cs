using DocxTemplater;
using Microsoft.AspNetCore.Http;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Dokumenti.Modeli;
using System.Security.Claims;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.OsiguranjeServisi
{
    public class GenerisiPolisuOsiguranjaServis
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;
        private readonly IOsiguranjeRepozitorijum _osiguranjeRepozitorijum;
        private readonly ITipOsiguranjaRepozitorijum _tipOsiguranjaRepozitorijum;
        private readonly IHttpContextAccessor _httpKontekst;

        public GenerisiPolisuOsiguranjaServis(
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum,
            IOsiguranjeRepozitorijum osiguranjeRepozitorijum,
            ITipOsiguranjaRepozitorijum tipOsiguranjaRepozitorijum,
            IHttpContextAccessor httpKontekst)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
            _osiguranjeRepozitorijum = osiguranjeRepozitorijum;
            _tipOsiguranjaRepozitorijum = tipOsiguranjaRepozitorijum;
            _httpKontekst = httpKontekst;
        }

        public async Task<string> Generisi(string brPolise)
        {
            var korisnickePretpostavke = _httpKontekst
                .HttpContext!
                .User;
            var korisnikIdPretpostavka = korisnickePretpostavke
                .FindFirst(ClaimTypes.NameIdentifier) ??
                korisnickePretpostavke
                .FindFirst("iss");

            var korisnikId = korisnikIdPretpostavka!.Value;

            var korisnik = await _korisnikRepozitorijum
                .VratiPoIdAsync(Guid.Parse(korisnikId));

            var motocikl = await _motociklADORepozitorijum
                .VratiPoKorisnikIdAsync(korisnik.Id);

            var osiguranje = await _osiguranjeRepozitorijum
                .VratiPoBrPoliseAsync(brPolise);

            var tipOsiguranja = await _tipOsiguranjaRepozitorijum
                .VratiPoIdAsync(osiguranje.TipOsiguranjaId);

            var korisnikDokument = new KorisnikDokument
            {
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                JMBG = korisnik.JMBG,
                BrVozackeDozvole = korisnik.BrVozackeDozvole,
                Adresa = korisnik.Adresa,
                Telefon = korisnik.Telefon
            };

            var motociklDokument = new MotociklDokument
            {
                BrendModel = motocikl.BrendModel,
                GodinaProizvodnje = motocikl.GodinaProizvodnje,
                BrSaobracajneDozvole = motocikl.BrSaobracajneDozvole,
                PrimarnaUpotreba = motocikl.PrimarnaUpotreba
            };

            var osiguranjeDokument = new OsiguranjeDokument
            {
                BrPolise = osiguranje.BrPolise,
                GodisnjaPremija = osiguranje.GodisnjaPremija
            };

            var tipOsiguranjaDokument = new TipOsiguranjaDokument
            {
                NazivOsiguranja = tipOsiguranja.NazivOsiguranja
            };

            var polisa = new Polisa
            {
                Korisnik = korisnikDokument,
                Motocikl = motociklDokument,
                Osiguranje = osiguranjeDokument,
                TipOsiguranja = tipOsiguranjaDokument
            };
            var sablonPutanja = Path.Combine(AppContext.BaseDirectory, "Dokumenti", "PolisaOsiguranja.docx");

            if (!File.Exists(sablonPutanja))
            {
                throw new FileNotFoundException("Šablon nije pronađen:", sablonPutanja);
            }

            var izlazniFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Polise");
            Directory.CreateDirectory(izlazniFolder);

            var imeFajla = $"Polisa_{osiguranje.BrPolise}_{Guid.NewGuid():N}.docx";
            var izlaznaPutanja = Path.Combine(izlazniFolder, imeFajla);

            using (var docxSablon = DocxTemplate.Open(sablonPutanja))
            {
                docxSablon.BindModel("p", polisa);

                using var izlazniStream = File.Create(izlaznaPutanja);
                docxSablon.Save(izlazniStream);
            }

            return imeFajla;
        }
    }
}
