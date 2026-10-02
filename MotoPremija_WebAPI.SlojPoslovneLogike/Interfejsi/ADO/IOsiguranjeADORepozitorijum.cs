

using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO
{
    public interface IOsiguranjeADORepozitorijum
    {
        Task<Osiguranje?> VratiPoIdAsync(Guid id);
        Task<List<Osiguranje>> VratiSvePoMotociklIdAsync(Guid motociklId);
        Task<bool> KreirajAsync(Osiguranje osiguranje);
        Task<bool> ObrisiAsync(Guid id);
    }
}
