using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoPremija_WebAPI.SlojServisa.Servisi.TipOsiguranjaServisi;

namespace MotoPremija_WebAPI.Kontroleri
{
    [ApiController]
    [Route("/tip-osiguranja")]
    public class TipOsiguranjaKontroler : ControllerBase
    {
        private readonly VratiTipoveOsiguranjaServis _vratiTipoveOsiguranjaServis;

        public TipOsiguranjaKontroler(VratiTipoveOsiguranjaServis vratiTipoveOsiguranjaServis)
        {
            _vratiTipoveOsiguranjaServis = vratiTipoveOsiguranjaServis;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> VratiTipoveOsiguranja()
        {
            var tipoviOsiguranjaDTO = await _vratiTipoveOsiguranjaServis.Vrati();
            return Ok(tipoviOsiguranjaDTO);
        }
    }
}
