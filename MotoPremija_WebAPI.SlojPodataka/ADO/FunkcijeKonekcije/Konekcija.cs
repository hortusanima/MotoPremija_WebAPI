using Npgsql;
using System.Data;

namespace MotoPremija_WebAPI.SlojPodataka.ADO.FunkcijeKonekcije
{
    public class Konekcija(string konekcioniString)
    {
        private NpgsqlConnection _konekcija;
        private readonly string _konekcioniString = konekcioniString;

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

        public async Task ZatvoriKonekcijuAsync()
        {
            if (_konekcija != null && _konekcija.State == ConnectionState.Open)
            {
                await _konekcija.CloseAsync();
                await _konekcija.DisposeAsync();
            }
        }
    }
}
