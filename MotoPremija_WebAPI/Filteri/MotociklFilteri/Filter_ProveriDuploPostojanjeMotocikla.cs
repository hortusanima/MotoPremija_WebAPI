using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoPremija_WebAPI.PomocneFunkcije;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Motocikl;
using System.Security.Claims;

namespace MotoPremija_WebAPI.Filteri.MotociklFilteri
{
    public class Filter_ProveriDuploPostojanjeMotocikla : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpKontekst;
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;
        private readonly IMotociklRepozitorijum _motociklRepozitorijum;

        public Filter_ProveriDuploPostojanjeMotocikla(
            IHttpContextAccessor httpKontekst,
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum,
            IMotociklRepozitorijum motociklRepozitorijum
            )
        {
            _httpKontekst = httpKontekst;
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
            _motociklRepozitorijum = motociklRepozitorijum;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext kontekst, ActionExecutionDelegate sledeci)
        {
            var motociklDTO = kontekst
                 .ActionArguments["motociklDTO"] as MotociklDTO;

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

            var korisnikovMotocikl = await _motociklADORepozitorijum
                .VratiPoKorisnikIdAsync(korisnik.Id);

            var unetMotocikl = await _motociklRepozitorijum
                .VratiPoBrSaobracajneDozvoleAsync(motociklDTO.BrSaobracajneDozvole);

            if (korisnikovMotocikl != null || unetMotocikl != null)
            {
                FunkcijeStatusaGreske
                    .KreirajStatusGreske(
                    kontekst,
                    "Motocikl",
                    "Ovaj motocikl je već osiguran.",
                    409,
                    detaljiGreske =>
                    new ConflictObjectResult(detaljiGreske));

                return;
            }
            else
            {
                await sledeci();
            }
        }
    }
}
