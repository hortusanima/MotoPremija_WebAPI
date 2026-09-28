using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoPremija_WebAPI.Filteri.KorisnikFilteri;
using MotoPremija_WebAPI.Filteri.MotociklFilteri;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Motocikl;
using MotoPremija_WebAPI.SlojServisa.Servisi.MotociklServisi;

namespace MotoPremija_WebAPI.Kontroleri
{
    [ApiController]
    [Route("/motocikli")]
    public class MotociklKontroler : ControllerBase
    {
        private readonly DodajMotociklServis _dodajMotociklServis;
        private readonly VratiMotociklServis _vratiMotociklServis;
        private readonly AzurirajMotociklServis _azurirajMotociklServis;
        private readonly ObrisiMotociklServis _obrisiMotociklServis;

        public MotociklKontroler(
            DodajMotociklServis dodajMotociklServis,
            VratiMotociklServis vratiMotociklServis,
            AzurirajMotociklServis azurirajMotociklServis,
            ObrisiMotociklServis obrisiMotociklServis)
        {
            _dodajMotociklServis = dodajMotociklServis;
            _vratiMotociklServis = vratiMotociklServis;
            _azurirajMotociklServis = azurirajMotociklServis;
            _obrisiMotociklServis = obrisiMotociklServis;
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(Filter_ProveriDuploPostojanjeMotocikla))]
        public async Task<IActionResult> DodajMotocikl([FromBody] MotociklDTO motociklDTO)
        {
            await _dodajMotociklServis.Dodaj(motociklDTO);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(Filter_ProveriDaLiMotociklPostoji))]
        public async Task<IActionResult> VratiMotocikl()
        {
            var motociklDTO = await _vratiMotociklServis
                .Vrati();
            return Ok(motociklDTO);
        }

        [HttpPut]
        [Authorize]
        [TypeFilter(typeof(Filter_ProveriDaLiMotociklPostoji))]
        public async Task<IActionResult> AzurirajMotocikl([FromBody] AzuriranMotociklDTO motociklDTO)
        {
            await _azurirajMotociklServis.Azuriraj(motociklDTO);
            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        [TypeFilter(typeof(Filter_ProveriAdministrativnuDozvoluKorisnika))]
        [TypeFilter(typeof(Filter_ProveriDaLiMotociklPostoji))]
        public async Task<IActionResult> ObrisiMotocikl()
        {
            await _obrisiMotociklServis.Obrisi();
            return NoContent();
        }
    }
}
