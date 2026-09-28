using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MotoPremija_WebAPI.Filteri.GlobalniFilteri
{
    public class Filter_ResiIzuzetakServerskeGreske : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext kontekst)
        {
            var errorResponse = new
            {
                Error = "Nastala je greška tokom procesuiranja zahteva."
            };
            kontekst.Result = new ObjectResult(errorResponse)
            {
                StatusCode = 500
            };
            kontekst.ExceptionHandled = true;
            await Task.CompletedTask;
        }
    }
}
