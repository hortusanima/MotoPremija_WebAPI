namespace MotoPremija_WebAPI.SlojPodataka.ADO.FunkcijeKonekcije
{
    public class Konekcija : BaznaKonekcija
    {
        public Konekcija(string konekcioniString) : base(konekcioniString)
        {
        }
        public override async Task ZatvoriKonekcijuAsync()
        {
            await base.ZatvoriKonekcijuAsync();

            _konekcija = null;
        }
    }
}
