
using Microsoft.AspNetCore.Http;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojPoslovneLogike.PoslovnaLogika;
using MotoPremija_WebAPI.SlojServisa.PomocneFunkcije;
using System.Security.Claims;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.OsiguranjeServisi
{
    public class KreirajOsiguranjeServis
    {

        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;
        private readonly IOsiguranjeADORepozitorijum _osiguranjeADORepozitorijum;
        private readonly ITipOsiguranjaRepozitorijum _tipOsiguranjaRepozitorijum;
        private readonly IHttpContextAccessor _httpKontekst;

        public KreirajOsiguranjeServis(
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum,
            IOsiguranjeADORepozitorijum osiguranjeADORepozitorijum,
            ITipOsiguranjaRepozitorijum tipOsiguranjaRepozitorijum,
            IHttpContextAccessor httpKontekst)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
            _osiguranjeADORepozitorijum = osiguranjeADORepozitorijum;
            _tipOsiguranjaRepozitorijum = tipOsiguranjaRepozitorijum;
            _httpKontekst = httpKontekst;
        }

        public async Task Kreiraj(string naziv)
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

            var faktorStarosti = new LogikaFaktoraPoGodiniProizvodnje(
                motocikl.GodinaProizvodnje
            ).VratiFaktorStarosti();

            var taksaPrimarneUpotrebe = new LogikaTaksePoPrimarnojUpotrebi(
                motocikl.PrimarnaUpotreba
            ).VratiTaksuUpotrebe();

            var tipOsiguranja = await _tipOsiguranjaRepozitorijum
                .VratiPoNazivuAsync(naziv);

            var godisnjaPremija = new LogikaGodisnjePremije(
                tipOsiguranja.BaznaPremija,
                faktorStarosti,
                taksaPrimarneUpotrebe
            ).IzracunajGodisnjuPremiju();

            var osiguranje = new Osiguranje
            {
                Id = Guid.NewGuid(),
                BrPolise = FunkcijeBrojaPolise.GenerisiBrojPolise(),
                GodisnjaPremija = godisnjaPremija,
                MotociklId = motocikl.Id,
                TipOsiguranjaId = tipOsiguranja.Id
            };

            await _osiguranjeADORepozitorijum
                .KreirajAsync(osiguranje);
        }
    }
}
