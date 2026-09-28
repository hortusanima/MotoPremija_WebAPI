using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoPremija_WebAPI.Filteri.KorisnikFilteri;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik;
using MotoPremija_WebAPI.SlojServisa.Servisi.KorisnikServisi;

namespace MotoPremija_WebAPI.Kontroleri
{
    [ApiController]
    [Route("/korisnici")]
    public class KorisnikKontroler(
        RegistracijaServis registracijaServis,
        PrijavaServis prijavaServis,
        VratiKorisnikaServis vratiKorisnikaServis) : ControllerBase
    {
        private readonly RegistracijaServis _registracijaServis = registracijaServis;
        private readonly PrijavaServis _prijavaServis = prijavaServis;
        private readonly VratiKorisnikaServis _vratiKorisnikaServis = vratiKorisnikaServis;


        [HttpPost("registracija")]
        [TypeFilter(typeof(Filter_ProveriDuploPostojanjeKorisnika))]
        public async Task<IActionResult> RegistrujKorisnika([FromBody] RegistracijaDTO registracijaDTO)
        {
            var registrovaniKorisnik = await _registracijaServis
                .Registruj(registracijaDTO);

            return Ok(registrovaniKorisnik);
        }

        [HttpPost("prijava")]
        [TypeFilter(typeof(Filter_ProveriKredencijaleKorisnika))]
        public async Task<IActionResult> PrijaviKorisnika([FromBody] PrijavaDTO prijavaDTO)
        {
            var jwt = await _prijavaServis
                .Prijavi(prijavaDTO);

            return Ok(new
            {
                AccessToken = jwt,
                TokenType = "Bearer",
                ExpiresIn = 3600
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> VratiKorisnika()
        {
            var korisnik = await _vratiKorisnikaServis.Vrati();

            return Ok(korisnik);
        }
    }
}
