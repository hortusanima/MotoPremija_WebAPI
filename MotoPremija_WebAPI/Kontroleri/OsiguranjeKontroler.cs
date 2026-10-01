using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoPremija_WebAPI.Filteri.MotociklFilteri;
using MotoPremija_WebAPI.Filteri.OsiguranjeFilteri;
using MotoPremija_WebAPI.Filteri.TipOsiguranjaFilteri;
using MotoPremija_WebAPI.SlojServisa.Servisi.OsiguranjeServisi;

namespace MotoPremija_WebAPI.Kontroleri
{
    [ApiController]
    [Route("/osiguranja")]
    public class OsiguranjeKontroler : ControllerBase
    {
        private readonly VratiSvaOsiguranjaServis _vratiSvaOsiguranjaServis;
        private readonly KreirajOsiguranjeServis _kreirajOsiguranjeServis;
        private readonly GenerisiPolisuOsiguranjaServis _generisiPolisuOsiguranjaServis;

        public OsiguranjeKontroler(
            VratiSvaOsiguranjaServis vratiSvaOsiguranjaServis,
            KreirajOsiguranjeServis kreirajOsiguranjeServis,
            GenerisiPolisuOsiguranjaServis generisiPolisuOsiguranjaServis)
        {
            _vratiSvaOsiguranjaServis = vratiSvaOsiguranjaServis;
            _kreirajOsiguranjeServis = kreirajOsiguranjeServis;
            _generisiPolisuOsiguranjaServis = generisiPolisuOsiguranjaServis;
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
        [TypeFilter(typeof(Filter_ProveriNazivTipaOsiguranja))]
        [TypeFilter(typeof(Filter_ProveriDuploPostojanjeOsiguranja))]
        public async Task<IActionResult> KreirajOsiguranje([FromBody] string tipOsiguranja)
        {
            await _kreirajOsiguranjeServis
                .Kreiraj(tipOsiguranja);
            return Ok();
        }

        [HttpPost("dokument")]
        [Authorize]
        [TypeFilter(typeof(Filter_ProveriDaLiMotociklPostoji))]
        public async Task<IActionResult> KreirajDokument([FromBody] string brPolise)
        {
            string imeFajla = await _generisiPolisuOsiguranjaServis.Generisi(brPolise);

            string downloadUrl = $"{Request.Scheme}://{Request.Host}/polise/{imeFajla}";

            return Ok(new { DownloadUrl = downloadUrl });
        }
    }
}
