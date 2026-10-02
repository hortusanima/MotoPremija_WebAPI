using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore
{
    public interface IMotociklRepozitorijum
    {
        Task<Motocikl> VratiPoIdAsync(Guid id);
        Task<Motocikl> VratiPoKorisnikIdAsync(Guid korisnikId);
        Task<Motocikl> VratiPoBrSaobracajneDozvoleAsync(string broj);
        Task KreirajAsync(Motocikl motocikl);
        Task AzurirajAsync(Motocikl motocikl);
        Task ObrisiAsync(Guid id);
    }
}
