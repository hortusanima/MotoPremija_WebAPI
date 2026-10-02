

namespace MotoPremija_WebAPI.SlojServisa.Modeli_DTO.Korisnik
{
    public record VracenKorisnikDTO
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string JMBG { get; set; }
        public string BrVozackeDozvole { get; set; }
    }
}
