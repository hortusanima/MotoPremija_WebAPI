
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik;
using MotoPremija_WebAPI.SlojServisa.PomocneMetode;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.KorisnikServisi
{
    public class PrijavaServis
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;
        private readonly FunkcijeJWTokena _funkcijeJWTokena;

        public PrijavaServis(
            IKorisnikRepozitorijum korisnikRepozitorijum,
            FunkcijeJWTokena funkcijeJWTokena)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
            _funkcijeJWTokena = funkcijeJWTokena;
        }

        public async Task<string> Prijavi(PrijavaDTO prijavaDTO)
        {
            var korisnik = await _korisnikRepozitorijum
                .VratiPoImejluAsync(prijavaDTO.Imejl);

            return _funkcijeJWTokena.GenerisiToken(korisnik!.Id.ToString());
        }
    }
}
