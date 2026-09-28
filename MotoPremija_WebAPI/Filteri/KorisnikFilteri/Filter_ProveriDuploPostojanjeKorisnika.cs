using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoPremija_WebAPI.PomocneFunkcije;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik;

namespace MotoPremija_WebAPI.Filteri.KorisnikFilteri
{
    public class Filter_ProveriDuploPostojanjeKorisnika(IKorisnikRepozitorijum korisnikRepozitorijum) : IAsyncActionFilter
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum = korisnikRepozitorijum;
        public async Task OnActionExecutionAsync(
            ActionExecutingContext kontekst, 
            ActionExecutionDelegate sledeci
        )
        {
            var registracijaDTO = kontekst
                .ActionArguments["registracijaDTO"] as RegistracijaDTO;

            var postojeciImejl = await _korisnikRepozitorijum
                .VratiPoImejluAsync(registracijaDTO!.Imejl!);

            var postojeciJMBG = await _korisnikRepozitorijum
                .VratiPoJMBGAsync(registracijaDTO!.JMBG!);

            var postojeciBrVozackeDozvole = await _korisnikRepozitorijum
                .VratiPoImejluAsync(registracijaDTO!.BrVozackeDozvole);

            if (postojeciImejl != null || postojeciJMBG != null || postojeciBrVozackeDozvole != null)
            {
                FunkcijeStatusaGreske
                    .KreirajStatusGreske(
                    kontekst,
                    "Korisnik",
                    "Korisnik već postoji.",
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
