
using Microsoft.EntityFrameworkCore;
using MotoPremija_WebAPI.SlojPodataka.EFCore.Kontekst;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;

namespace MotoPremija_WebAPI.SlojPodataka.EFCore.Repozitorijumi
{
    public class KorisnikRepozitorijum : IKorisnikRepozitorijum
    {
        private readonly KontekstBazeAplikacije _kontekst;

        public KorisnikRepozitorijum(KontekstBazeAplikacije kontekst)
        {
            _kontekst = kontekst;
        }
        public async Task<Korisnik?> VratiPoIdAsync(Guid id)
        {
            return await _kontekst.Korisnik
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<Korisnik?> VratiPoImejluAsync(string imejl)
        {
            return await _kontekst.Korisnik
                .FirstOrDefaultAsync(k => k.Imejl == imejl);
        }

        public async Task<Korisnik?> VratiPoJMBGAsync(string JMBG)
        {
            return await _kontekst.Korisnik
                .FirstOrDefaultAsync(k => k.JMBG == JMBG);
        }

        public async Task<Korisnik?> VratiPoBrVozackeDozvoleAsync(string broj)
        {
            return await _kontekst.Korisnik
                .FirstOrDefaultAsync(k => k.BrVozackeDozvole == broj);
        }
        public async Task KreirajAsync(Korisnik korisnik)
        {
            await _kontekst.Korisnik.AddAsync(korisnik);
            await _kontekst.SaveChangesAsync();
        }

        public async Task ObrisiAsync(Guid id)
        {
            var korisnik = await _kontekst.Korisnik.FindAsync(id);

            if (korisnik != null)
            {
                _kontekst.Korisnik.Remove(korisnik);
                await _kontekst.SaveChangesAsync();
            }
        }
    }
}
