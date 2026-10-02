using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.EFCore
{
    public interface IKorisnikRepozitorijum
    {
        Task<Korisnik?> VratiPoIdAsync(Guid id);
        Task<Korisnik?> VratiPoImejluAsync(string imejl);
        Task<Korisnik?> VratiPoJMBGAsync(string JMBG);
        Task<Korisnik?> VratiPoBrVozackeDozvoleAsync(string broj);
        Task KreirajAsync(Korisnik korisnik);
        Task ObrisiAsync(Guid id);
    }
}
