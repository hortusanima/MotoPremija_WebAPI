using Microsoft.AspNetCore.Http;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Motocikl;
using System.Security.Claims;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.MotociklServisi
{
    public class VratiMotociklServis
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;
        private readonly IHttpContextAccessor _httpKontekst;

        public VratiMotociklServis(
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum,
            IHttpContextAccessor httpKontekst)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
            _httpKontekst = httpKontekst;
        }

        public async Task<MotociklDTO> Vrati()
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

            var motociklDTO = new MotociklDTO
            {
                BrendModel = motocikl.BrendModel,
                BrSaobracajneDozvole = motocikl.BrSaobracajneDozvole,
                GodinaProizvodnje = motocikl.GodinaProizvodnje,
                PrimarnaUpotreba = motocikl.PrimarnaUpotreba
            };

            return motociklDTO;
        }
    }
}
