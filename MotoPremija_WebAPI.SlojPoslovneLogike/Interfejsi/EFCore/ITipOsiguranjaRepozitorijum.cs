using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore
{
    public interface ITipOsiguranjaRepozitorijum
    {
        Task<IEnumerable<TipOsiguranja>> VratiSveAsync();
        Task<TipOsiguranja> VratiPoIdAsync(Guid id);
        Task<TipOsiguranja> VratiPoNazivuAsync(string naziv);
    }
}
