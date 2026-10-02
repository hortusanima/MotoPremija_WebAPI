
using Microsoft.AspNetCore.Http;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik;
using System.Security.Claims;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.KorisnikServisi
{
    public class VratiKorisnikaServis
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IHttpContextAccessor _httpKontekst;

        public VratiKorisnikaServis(
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IHttpContextAccessor httpKontekst)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _httpKontekst = httpKontekst;
        }

        public async Task<VracenKorisnikDTO> Vrati()
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

            return new VracenKorisnikDTO
            {
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                JMBG = korisnik.JMBG,
                BrVozackeDozvole = korisnik.BrVozackeDozvole
            };
        }
    }
}
