
using Microsoft.EntityFrameworkCore;
using MotoPremija_WebAPI.SlojPodataka.EFCore.Kontekst;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;

namespace MotoPremija_WebAPI.SlojPodataka.EFCore.Repozitorijumi
{
    public class MotociklRepozitorijum : IMotociklRepozitorijum
    {
        private readonly KontekstBazeAplikacije _kontekst;

        public MotociklRepozitorijum(KontekstBazeAplikacije kontekst)
        {
            _kontekst = kontekst;
        }
        public async Task<Motocikl> VratiPoIdAsync(Guid id)
        {
            return await _kontekst.Motocikl
                .Include(m => m.Korisnik)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Motocikl> VratiPoKorisnikIdAsync(Guid korisnikId)
        {
            return await _kontekst.Motocikl
                .Include(m => m.Korisnik)
                .FirstOrDefaultAsync(m => m.KorisnikId == korisnikId);
        }

        public async Task<Motocikl> VratiPoBrSaobracajneDozvoleAsync(string broj)
        {
            return await _kontekst.Motocikl
                .Include(m => m.Korisnik)
                .FirstOrDefaultAsync(m => m.BrSaobracajneDozvole == broj);
        }

        public async Task KreirajAsync(Motocikl motocikl)
        {
            await _kontekst.Motocikl.AddAsync(motocikl);
            await _kontekst.SaveChangesAsync();
        }

        public async Task AzurirajAsync(Motocikl motocikl)
        {
            _kontekst.Motocikl.Update(motocikl);
            await _kontekst.SaveChangesAsync();
        }

        public async Task ObrisiAsync(Guid id)
        {
            var motocikl = await _kontekst.Motocikl.FindAsync(id);

            if (motocikl != null)
            {
                _kontekst.Motocikl.Remove(motocikl);
                await _kontekst.SaveChangesAsync();
            }
        }
    }
}
