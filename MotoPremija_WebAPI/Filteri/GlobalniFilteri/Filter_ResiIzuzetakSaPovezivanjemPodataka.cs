using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace MotoPremija_WebAPI.Filteri.GlobalniFilteri
{
    public class Filter_ResiIzuzetakSaPovezivanjemPodataka : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext kontekst)
        {
            if (kontekst.Exception is JsonException ||
               kontekst.Exception is FormatException)
            {
                var detaljiIzuzetka = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Nepravilan format podataka.",
                    Detail = "Jedna ili više vrednosti su u nepravilnom formatu."
                };

                if (kontekst.Exception is JsonException jsonIzuzetak)
                {
                    detaljiIzuzetka.Detail += $" Greška: {jsonIzuzetak.Message}";
                }
                else if (kontekst.Exception is FormatException formatIzuzetak)
                {
                    detaljiIzuzetka.Detail += $" Greška: {formatIzuzetak.Message}";
                }

                kontekst.Result = new BadRequestObjectResult(detaljiIzuzetka);

                kontekst.ExceptionHandled = true;
                await Task.CompletedTask;
            }
        }
    }
}
