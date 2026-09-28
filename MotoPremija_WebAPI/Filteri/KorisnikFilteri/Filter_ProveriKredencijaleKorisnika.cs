using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoPremija_WebAPI.PomocneFunkcije;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik;
using MotoPremija_WebAPI.SlojServisa.PomocneMetode;

namespace MotoPremija_WebAPI.Filteri.KorisnikFilteri
{
    public class Filter_ProveriKredencijaleKorisnika(IKorisnikRepozitorijum korisnikRepozitorijum) : IAsyncActionFilter
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum = korisnikRepozitorijum;

        public async Task OnActionExecutionAsync(ActionExecutingContext kontekst, ActionExecutionDelegate sledeci)
        {
            var prijavaDTO = kontekst.ActionArguments["prijavaDTO"] as PrijavaDTO;

            var korisnik = await _korisnikRepozitorijum
                .VratiPoImejluAsync(prijavaDTO!.Imejl);

            if (korisnik == null)
            {
                FunkcijeStatusaGreske
                    .KreirajStatusGreske(
                    kontekst,
                    "Korisnik",
                    "Neispravni kredencijali.",
                    409,
                    detaljiGreske =>
                    new UnauthorizedObjectResult(detaljiGreske));

                return;
            }
            else if (korisnik != null && !FunkcijeLozinke.ProveriLozinku(
                prijavaDTO.Lozinka, 
                korisnik.LozinkaSalt, 
                korisnik.LozinkaHash)
            )
            {
                FunkcijeStatusaGreske
                   .KreirajStatusGreske(
                   kontekst,
                   "Korisnik",
                   "Neispravni kredencijali.",
                   409,
                   detaljiGreske =>
                   new UnauthorizedObjectResult(detaljiGreske));

                return;
            }
            else
            {
                await sledeci();
            }
        }
    }
}
