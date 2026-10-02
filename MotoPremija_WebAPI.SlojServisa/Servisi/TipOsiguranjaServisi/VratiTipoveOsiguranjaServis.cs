using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore;
using MotoPremija_WebAPI.SlojServisa.Modeli_DTO.TipOsiguranja;

namespace MotoPremija_WebAPI.SlojServisa.Servisi.TipOsiguranjaServisi
{
    public class VratiTipoveOsiguranjaServis
    {
        private readonly ITipOsiguranjaRepozitorijum  _tipOsiguranjaRepozitorijum;

        public VratiTipoveOsiguranjaServis(ITipOsiguranjaRepozitorijum tipOsiguranjaRepozitorijum)
        {
            _tipOsiguranjaRepozitorijum = tipOsiguranjaRepozitorijum;
        }

        public async Task<List<TipOsiguranjaDTO>> Vrati()
        {
            var tipoviOsiguranja = await _tipOsiguranjaRepozitorijum
                .VratiSveAsync();

            var tipoviOsiguranjaDTO = new List<TipOsiguranjaDTO>();

            foreach(var tip in tipoviOsiguranja)
            {
                var tipDTO = new TipOsiguranjaDTO
                {
                    NazivOsiguranja = tip.NazivOsiguranja,
                };
                tipoviOsiguranjaDTO.Add(tipDTO);
            }

            return tipoviOsiguranjaDTO;
        }

    }
}
