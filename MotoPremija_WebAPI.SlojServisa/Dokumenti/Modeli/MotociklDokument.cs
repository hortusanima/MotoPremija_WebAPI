using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;

namespace MotoPremija_WebAPI.SlojServisa.Dokumenti.Modeli
{
    public class MotociklDokument
    {
        public string BrendModel { get; set; }
        public int GodinaProizvodnje { get; set; }
        public string BrSaobracajneDozvole { get; set; }
        public PrimarnaUpotreba PrimarnaUpotreba { get; set; }
    }
}
