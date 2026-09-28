using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MotoPremija_WebAPI.PomocneFunkcije
{
    public static class FunkcijeStatusaGreske
    {
        public static void KreirajStatusGreske<TResult>(
            ActionExecutingContext kontekst,
            string kljuc,
            string poruka,
            int statusniKod,
            Func<ValidationProblemDetails, TResult> rezultat)
            where TResult :
            ObjectResult
        {
            kontekst.ModelState
                .AddModelError(
                kljuc,
                poruka);

            var detaljiGreske =
            new ValidationProblemDetails(kontekst.ModelState)
            {
                Status = statusniKod
            };

            kontekst.Result = rezultat(detaljiGreske);
        }
    }
}
