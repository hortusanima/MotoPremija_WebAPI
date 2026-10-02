using Microsoft.AspNetCore.Http;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Motocikl;
using System.Security.Claims;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.MotociklServisi
{
    public class DodajMotociklServis
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;
        private readonly IHttpContextAccessor _httpKontekst;

        public DodajMotociklServis(
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum,
            IHttpContextAccessor httpKontekst)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
            _httpKontekst = httpKontekst;
        }

        public async Task Dodaj(MotociklDTO motociklDTO)
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

            var motocikl = new Motocikl
            {
                Id = Guid.NewGuid(),
                BrendModel = motociklDTO.BrendModel,
                PrimarnaUpotreba = motociklDTO.PrimarnaUpotreba,
                GodinaProizvodnje = motociklDTO.GodinaProizvodnje,
                BrSaobracajneDozvole = motociklDTO.BrSaobracajneDozvole,
                KorisnikId = korisnik.Id
            };

            await _motociklADORepozitorijum
                .KreirajAsync(motocikl);
        }
    }
}
