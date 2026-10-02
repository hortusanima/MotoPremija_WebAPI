
using Microsoft.EntityFrameworkCore;
using MotoPremija_WebAPI.SlojPodataka.EFCore.Kontekst;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;

namespace MotoPremija_WebAPI.SlojPodataka.EFCore.Repozitorijumi
{
    public class OsiguranjeRepozitorijum : IOsiguranjeRepozitorijum
    {
        private readonly KontekstBazeAplikacije _kontekst;

        public OsiguranjeRepozitorijum(KontekstBazeAplikacije kontekst)
        {
            _kontekst = kontekst;
        }
        public async Task<Osiguranje> VratiPoIdAsync(Guid id)
        {
            return await _kontekst.Osiguranje
                .Include(o => o.Motocikl)
                    .ThenInclude(m => m.Korisnik)
                .Include(o => o.TipOsiguranja)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Osiguranje> VratiPoBrPoliseAsync(string broj)
        {
            return await _kontekst.Osiguranje
                .FirstOrDefaultAsync(o => o.BrPolise == broj);
        }
        public async Task<IEnumerable<Osiguranje>> VratiSvePoMotociklIdAsync(Guid motociklId)
        {
            return await _kontekst.Osiguranje
                .Include(o => o.TipOsiguranja)
                .Where(o => o.MotociklId == motociklId)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task KreirajAsync(Osiguranje osiguranje)
        {
            await _kontekst.Osiguranje.AddAsync(osiguranje);
            await _kontekst.SaveChangesAsync();
        }
        public async Task ObrisiAsync(Guid id)
        {
            var osiguranje = await _kontekst.Osiguranje.FindAsync(id);

            if (osiguranje != null)
            {
                _kontekst.Osiguranje.Remove(osiguranje);
                await _kontekst.SaveChangesAsync();
            }
        }
    }
}
