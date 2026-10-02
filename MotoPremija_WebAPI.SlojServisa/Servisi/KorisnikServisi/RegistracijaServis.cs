using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik;
using MotoPremija_WebAPI.SlojServisa.PomocneMetode;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.KorisnikServisi
{
    public class RegistracijaServis
    {
        private readonly IKorisnikRepozitorijum _korisnikRepozitorijum;

        public RegistracijaServis(IKorisnikRepozitorijum korisnikRepozitorijum)
        {
            _korisnikRepozitorijum = korisnikRepozitorijum;
        }

        public async Task<VracenKorisnikDTO> Registruj(RegistracijaDTO registracijaDTO)
        {
            var lozinkaSalt = FunkcijeLozinke.GenerisiSalt();
            var lozinkaHash = FunkcijeLozinke.IzracunajHash(registracijaDTO.Lozinka, lozinkaSalt);

            var noviKorisnik = new Korisnik
            {
                Id = Guid.NewGuid(),
                Ime = registracijaDTO.Ime,
                Prezime = registracijaDTO.Prezime,
                JMBG = registracijaDTO.JMBG,
                Imejl = registracijaDTO.Imejl,
                Uloga = UlogaKorisnika.OSIGURANIK,
                Adresa = registracijaDTO.Adresa,
                Telefon = registracijaDTO.Telefon,
                BrVozackeDozvole = registracijaDTO.BrVozackeDozvole,
                LozinkaSalt = lozinkaSalt,
                LozinkaHash = lozinkaHash
            };

            await _korisnikRepozitorijum.KreirajAsync(noviKorisnik);

            return new VracenKorisnikDTO
            {
                Ime = noviKorisnik.Ime,
                Prezime = noviKorisnik.Prezime,
                JMBG = noviKorisnik.JMBG
            };
        }
    }
}
