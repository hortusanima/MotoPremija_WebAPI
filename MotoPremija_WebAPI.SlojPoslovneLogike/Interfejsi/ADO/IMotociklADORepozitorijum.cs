
using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO
{
    public interface IMotociklADORepozitorijum
    {
        Task<Motocikl?> VratiPoKorisnikIdAsync(Guid korisnikId);
        Task<bool> KreirajAsync(Motocikl motocikl);
        Task<bool> AzurirajAsync(Motocikl motocikl);
        Task<bool> ObrisiAsync(Guid id);
    }
}
