using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoPremija_WebAPI.Filteri.MotociklFilteri;
using MotoPremija_WebAPI.Filteri.OsiguranjeFilteri;
using MotoPremija_WebAPI.SlojServisa.Servisi.OsiguranjeServisi;

namespace MotoPremija_WebAPI.Kontroleri
{
    [ApiController]
    [Route("/osiguranja")]
    public class OsiguranjeKontroler : ControllerBase
    {
        private readonly VratiSvaOsiguranjaServis _vratiSvaOsiguranjaServis;
        private readonly KreirajOsiguranjeServis _kreirajOsiguranjeServis;

        public OsiguranjeKontroler(
            VratiSvaOsiguranjaServis vratiSvaOsiguranjaServis,
            KreirajOsiguranjeServis kreirajOsiguranjeServis)
        {
            _vratiSvaOsiguranjaServis = vratiSvaOsiguranjaServis;
            _kreirajOsiguranjeServis = kreirajOsiguranjeServis;
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(Filter_ProveriDaLiMotociklPostoji))]
        public async Task<IActionResult> VratiSvaOsiguranja()
        {
            var svaOsiguranjaDTO = await _vratiSvaOsiguranjaServis
                .Vrati();
            return Ok(svaOsiguranjaDTO);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(Filter_ProveriDaLiMotociklPostoji))]
        [TypeFilter(typeof(Filter_ProveriDuploPostojanjeOsiguranja))]
        public async Task<IActionResult> KreirajOsiguranje([FromBody] string tipOsiguranja)
        {
            await _kreirajOsiguranjeServis
                .Kreiraj(tipOsiguranja);
            return Ok();
        }

        [HttpPost("dokument")]
        [Authorize]
        public async Task<IActionResult> KreirajDokument()
        {
            return Ok();
        }
    }
}
