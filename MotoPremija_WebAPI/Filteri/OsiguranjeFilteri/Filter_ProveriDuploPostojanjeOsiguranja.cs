using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoPremija_WebAPI.PomocneFunkcije;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using System.Security.Claims;

namespace MotoPremija_WebAPI.Filteri.OsiguranjeFilteri
{
    public class Filter_ProveriDuploPostojanjeOsiguranja : IAsyncActionFilter
    {
        private readonly IHttpContextAccessor _httpKontekst;
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly IMotociklADORepozitorijum _motociklADORepozitorijum;
        private readonly IOsiguranjeADORepozitorijum _osiguranjeADORepozitorijum;
        private readonly ITipOsiguranjaRepozitorijum _tipOsiguranjaRepozitorijum;

        public Filter_ProveriDuploPostojanjeOsiguranja(
            IHttpContextAccessor httpKontekst,
            IKorisnikRepozitorijum korisnikRepozitorijum,
            IMotociklADORepozitorijum motociklADORepozitorijum,
            IOsiguranjeADORepozitorijum osiguranjeADORepozitorijum,
            ITipOsiguranjaRepozitorijum tipOsiguranjaRepozitorijum
        )
        {
            _httpKontekst = httpKontekst;
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _motociklADORepozitorijum = motociklADORepozitorijum;
            _osiguranjeADORepozitorijum = osiguranjeADORepozitorijum;
            _tipOsiguranjaRepozitorijum = tipOsiguranjaRepozitorijum;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext kontekst, ActionExecutionDelegate sledeci)
        {
            var tipOsiguranja = kontekst
                 .ActionArguments["tipOsiguranja"] as string;

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

            var svaOsiguranja = await _osiguranjeADORepozitorijum
                .VratiSvePoMotociklIdAsync(korisnikovMotocikl.Id);

            bool postoji = false;

            foreach(var osiguranje in svaOsiguranja)
            {
                var tip = await _tipOsiguranjaRepozitorijum
                    .VratiPoIdAsync(osiguranje.TipOsiguranjaId);

                if(tip.NazivOsiguranja == tipOsiguranja)
                {
                    postoji = true;
                    break;
                }
            }

            if (postoji)
            {
                FunkcijeStatusaGreske
                    .KreirajStatusGreske(
                    kontekst,
                    "Osiguranje",
                    "Motocikl je već osiguran ovim osiguranjem.",
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
