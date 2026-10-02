
using MotoPremija_WebAPI.SlojPodataka.ADO.FunkcijeKonekcije;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using Npgsql;
using System.Data;

namespace MotoPremija_WebAPI.SlojPodataka.ADO.Repozitorijumi
{
    public class TipOsiguranjaADORepozitorijum : ITipOsiguranjaADORepozitorijum
    {
        private readonly string _konekcioniString;

        public TipOsiguranjaADORepozitorijum(string konekcioniString)
        {
            _konekcioniString = konekcioniString;
        }

        public async Task<List<TipOsiguranja>?> VratiSveAsync()
        {
            var lista = new List<TipOsiguranja>();
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return lista;

            try
            {
                await using var cmd = new NpgsqlCommand("SELECT id, nazivosiguranja, baznapremija FROM tiposiguranja", db.DajKonekciju());
                await using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    lista.Add(new TipOsiguranja
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        NazivOsiguranja = reader.GetString(reader.GetOrdinal("nazivosiguranja")),
                        BaznaPremija = reader.GetDouble(reader.GetOrdinal("baznapremija"))
                    });
                }
                return lista;
            }
            finally
            {
                await db.ZatvoriKonekcijuAsync();
            }
        }

        public async Task<bool> KreirajAsync(TipOsiguranja tip)
        {
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return false;

            try
            {
                await using var cmd = new NpgsqlCommand("spdodajtiposiguranja", db.DajKonekciju());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_nazivosiguranja", tip.NazivOsiguranja);
                cmd.Parameters.AddWithValue("p_baznapremija", tip.BaznaPremija);

                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                await db.ZatvoriKonekcijuAsync();
            }
        }
    }
}
