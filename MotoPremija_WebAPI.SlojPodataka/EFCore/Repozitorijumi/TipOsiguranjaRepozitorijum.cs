using Microsoft.EntityFrameworkCore;
using MotoPremija_WebAPI.SlojPodataka.EFCore.Kontekst;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;

namespace MotoPremija_WebAPI.SlojPodataka.EFCore.Repozitorijumi
{
    public class TipOsiguranjaRepozitorijum : ITipOsiguranjaRepozitorijum
    {
        private readonly KontekstBazeAplikacije _kontekst;

        public TipOsiguranjaRepozitorijum(KontekstBazeAplikacije kontekst)
        {
            _kontekst = kontekst;
        }

        public async Task<IEnumerable<TipOsiguranja>> VratiSveAsync()
        {
            return await _kontekst.TipOsiguranja
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TipOsiguranja> VratiPoIdAsync(Guid id)
        {
            return await _kontekst.TipOsiguranja
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TipOsiguranja> VratiPoNazivuAsync(string naziv)
        {
            return await _kontekst.TipOsiguranja
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.NazivOsiguranja == naziv);
        }
    }
}
