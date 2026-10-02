using Microsoft.AspNetCore.Http;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Osiguranje;
using System.Security.Claims;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.OsiguranjeServisi
{
    public class VratiSvaOsiguranjaServis
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;
        private readonly IOsiguranjeADORepozitorijum _osiguranjeADORepozitorijum;
        private readonly IHttpContextAccessor _httpKontekst;

        public VratiSvaOsiguranjaServis(
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum,
            IOsiguranjeADORepozitorijum osiguranjeADORepozitorijum,
            IHttpContextAccessor httpKontekst)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
            _osiguranjeADORepozitorijum = osiguranjeADORepozitorijum;
            _httpKontekst = httpKontekst;
        }

        public async Task<List<VracenoOsiguranjeDTO>> Vrati()
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

            var svaOsiguranja = await _osiguranjeADORepozitorijum
                .VratiSvePoMotociklIdAsync(motocikl.Id);

            var svaOsiguranjaDTO = new List<VracenoOsiguranjeDTO>();

            foreach(var osiguranje in svaOsiguranja)
            {
                var osiguranjeDTO = new VracenoOsiguranjeDTO
                {
                    BrPolise = osiguranje.BrPolise,
                    GodisnjaPremija = osiguranje.GodisnjaPremija,
                };
                svaOsiguranjaDTO.Add(osiguranjeDTO);
            }

            return svaOsiguranjaDTO;
        }
    }
}
