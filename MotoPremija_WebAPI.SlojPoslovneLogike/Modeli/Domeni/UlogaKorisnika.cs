
using System.Text.Json.Serialization;

namespace MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UlogaKorisnika
    {
        ADMIN,
        OSIGURANIK
    }
}
