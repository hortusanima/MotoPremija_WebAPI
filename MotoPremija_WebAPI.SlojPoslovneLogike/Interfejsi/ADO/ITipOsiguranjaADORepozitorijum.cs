

using MotoPremija_WebAPI.SlojPodataka.Modeli;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO
{
    public interface ITipOsiguranjaADORepozitorijum
    {
        Task<List<TipOsiguranja>?> VratiSveAsync();
        Task<bool> KreirajAsync(TipOsiguranja tip);
    }
}
