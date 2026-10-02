
using Npgsql;

namespace MotoPremija_WebAPI.SlojPodataka.ADO.FunkcijeKonekcije
{
    public class Transakcija
    {
        private readonly Konekcija _konekcijaObjekat;
        private NpgsqlTransaction _transakcijaObjekat;

        public Transakcija(Konekcija konekcijaObjekat)
        {
            _konekcijaObjekat = konekcijaObjekat;
        }

        public async Task<bool> ZapocniTransakcijuAsync()
        {
            try
            {
                _transakcijaObjekat = await _konekcijaObjekat
                    .DajKonekciju()
                    .BeginTransactionAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ZavrsiTransakcijuAsync(bool postojiGreska)
        {
            try
            {
                if (_transakcijaObjekat != null)
                {
                    if (!postojiGreska)
                        await _transakcijaObjekat.CommitAsync();
                    else
                        await _transakcijaObjekat.RollbackAsync();

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public NpgsqlTransaction DajTransakciju() => _transakcijaObjekat;
    }
}
