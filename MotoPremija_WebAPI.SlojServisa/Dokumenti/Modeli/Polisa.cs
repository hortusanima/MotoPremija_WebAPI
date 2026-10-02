namespace MotoPremija_WebAPI.SlojServisa.Dokumenti.Modeli
{
    public class Polisa
    {
        public KorisnikDokument Korisnik { get; set; }
        public MotociklDokument Motocikl { get; set; }
        public OsiguranjeDokument Osiguranje { get; set; }
        public TipOsiguranjaDokument TipOsiguranja { get; set; }
    }
}
