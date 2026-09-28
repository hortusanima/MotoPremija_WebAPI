using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoPremija_WebAPI.PomocneFunkcije;
using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using System.Security.Claims;

namespace MotoPremija_WebAPI.Filteri.KorisnikFilteri
{
    public class Filter_ProveriAdministrativnuDozvoluKorisnika : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpKontekst;
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;

        public Filter_ProveriAdministrativnuDozvoluKorisnika(
            IHttpContextAccessor httpKontekst,
            IKorisnikRepozitorijum korisnikRepozitorijum
            )
        {
            _httpKontekst = httpKontekst;
            _korisnikRepozitorijum = korisnikRepozitorijum;
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

            if (korisnik.Uloga != UlogaKorisnika.ADMIN)
            {
                FunkcijeStatusaGreske
                    .KreirajStatusGreske(
                    kontekst,
                    "Korisnik",
                    "Korisnik nema administrativnu dozvolu.",
                    403,
                    detaljiGreske =>
                    new ObjectResult(detaljiGreske));

                return;
            }
            else
            {
                await sledeci();
            }
        }
    }
}
