using Microsoft.AspNetCore.Http;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Motocikl;
using System.Security.Claims;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.MotociklServisi
{
    public class AzurirajMotociklServis
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;
        private readonly IHttpContextAccessor _httpKontekst;

        public AzurirajMotociklServis(
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum,
            IHttpContextAccessor httpKontekst)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
            _httpKontekst = httpKontekst;
        }

        public async Task Azuriraj(AzuriranMotociklDTO motociklDTO)
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

            var motociklZaAzuriranje = new Motocikl
            {
                Id = motocikl.Id,
                BrendModel = motociklDTO.BrendModel,
                GodinaProizvodnje = motociklDTO.GodinaProizvodnje,
                BrSaobracajneDozvole = motocikl.BrSaobracajneDozvole,
                PrimarnaUpotreba = motocikl.PrimarnaUpotreba,
                KorisnikId = korisnik.Id
            };

            await _motociklADORepozitorijum
                .AzurirajAsync(motociklZaAzuriranje);
        }
    }
}
