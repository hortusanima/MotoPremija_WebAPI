using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore
{
    public interface IOsiguranjeRepozitorijum
    {
        Task<Osiguranje> VratiPoIdAsync(Guid id);
        Task<Osiguranje> VratiPoBrPoliseAsync(string broj);
        Task<IEnumerable<Osiguranje>> VratiSvePoMotociklIdAsync(Guid motociklId);
        Task KreirajAsync(Osiguranje osiguranje);
        Task ObrisiAsync(Guid id);
    }
}
