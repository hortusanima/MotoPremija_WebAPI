
using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO
{
    public interface IKorisnikADORepozitorijum
    {
        Task<bool> KreirajAsync(Korisnik korisnik);
        Task<bool> ObrisiAsync(Guid id);
    }
}
