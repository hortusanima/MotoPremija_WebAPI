using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MotoPremija_WebAPI.PomocneFunkcije;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;

namespace MotoPremija_WebAPI.Filteri.TipOsiguranjaFilteri
{
    public class Filter_ProveriNazivTipaOsiguranja : IAsyncActionFilter
    {
        private readonly ITipOsiguranjaRepozitorijum _tipOsiguranjaRepozitorijum;

        public Filter_ProveriNazivTipaOsiguranja(ITipOsiguranjaRepozitorijum tipOsiguranjaRepozitorijum)
        {
            _tipOsiguranjaRepozitorijum = tipOsiguranjaRepozitorijum;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext kontekst, ActionExecutionDelegate sledeci)
        {
            var tipOsiguranja = kontekst
                 .ActionArguments["tipOsiguranja"] as string;

            var tip = await _tipOsiguranjaRepozitorijum
                .VratiPoNazivuAsync(tipOsiguranja);

            if (tip == null)
            {
                FunkcijeStatusaGreske
                    .KreirajStatusGreske(
                    kontekst,
                    "Tip Osiguranja",
                    "Neispravno unet tip osiguranja.",
                    400,
                    detaljiGreske =>
                    new BadRequestObjectResult(detaljiGreske));

                return;
            }
            else
            {
                await sledeci();
            }
        }
    }
}
