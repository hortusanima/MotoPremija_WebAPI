using Npgsql;
using System.Data;

namespace MotoPremija_WebAPI.SlojPodataka.ADO.FunkcijeKonekcije
{
    public class BaznaKonekcija
    {
        protected NpgsqlConnection _konekcija;
        protected readonly string _konekcioniString;

        public BaznaKonekcija(string konekcioniString)
        {
            _konekcioniString = konekcioniString;
        }

        public async Task<bool> OtvoriKonekcijuAsync()
        {
            try
            {
                _konekcija = new NpgsqlConnection(_konekcioniString);
                await _konekcija.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public NpgsqlConnection DajKonekciju() => _konekcija;

        public virtual async Task ZatvoriKonekcijuAsync()
        {
            if (_konekcija != null && _konekcija.State == ConnectionState.Open)
            {
                await _konekcija.CloseAsync();
                await _konekcija.DisposeAsync();
            }
        }
    }
}