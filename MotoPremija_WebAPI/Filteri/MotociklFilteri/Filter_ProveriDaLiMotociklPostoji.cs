using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoPremija_WebAPI.PomocneFunkcije;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using System.Security.Claims;

namespace MotoPremija_WebAPI.Filteri.MotociklFilteri
{
    public class Filter_ProveriDaLiMotociklPostoji : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpKontekst;
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;

        public Filter_ProveriDaLiMotociklPostoji(
            IHttpContextAccessor httpKontekst,
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum
            )
        {
            _httpKontekst = httpKontekst;
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext kontekst, ActionExecutionDelegate sledeci)
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

            if (motocikl == null)
            {
                FunkcijeStatusaGreske
                    .KreirajStatusGreske(
                    kontekst,
                    "Motocikl",
                    "Motocikl nije pronađen.",
                    404,
                    detaljiGreske =>
                    new NotFoundObjectResult(detaljiGreske));

                return;
            }
            else
            {
                await sledeci();
            }
        }
    }
}
