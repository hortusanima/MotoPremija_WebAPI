
using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using Npgsql;
using System.Data;
using MotoPremija_WebAPI.SlojPodataka.ADO.FunkcijeKonekcije;

namespace MotoPremija_WebAPI.SlojPodataka.ADO.Repozitorijumi
{
    public class MotociklADORepozitorijum : IMotociklADORepozitorijum
    {
        private readonly string _konekcioniString;

        public MotociklADORepozitorijum(string konekcioniString)
        {
            _konekcioniString = konekcioniString;
        }

        public async Task<Motocikl?> VratiPoKorisnikIdAsync(Guid korisnikId)
        {
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return null;

            try
            {
                await using var cmd = new NpgsqlCommand("SELECT * FROM spdajmotociklpokorisnikid(@p_korisnikid)", db.DajKonekciju());
                cmd.Parameters.AddWithValue("p_korisnikid", korisnikId);

                await using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Motocikl
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        BrendModel = reader.GetString(reader.GetOrdinal("brendmodel")),
                        GodinaProizvodnje = reader.GetInt32(reader.GetOrdinal("godinaproizvodnje")),
                        BrSaobracajneDozvole = reader.GetString(reader.GetOrdinal("brsaobracajnedozvole")),
                        PrimarnaUpotreba = Enum.Parse<PrimarnaUpotreba>(reader.GetString(reader.GetOrdinal("primarnaupotreba")), true),
                        KorisnikId = korisnikId
                    };
                }
                return null;
            }
            finally
            {
                await db.ZatvoriKonekcijuAsync();
            }
        }

        public async Task<bool> KreirajAsync(Motocikl motocikl)
        {
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return false;

            try
            {
                await using var cmd = new NpgsqlCommand("spdodajmotocikl", db.DajKonekciju());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_brendmodel", motocikl.BrendModel);
                cmd.Parameters.AddWithValue("p_godinaproizvodnje", motocikl.GodinaProizvodnje);
                cmd.Parameters.AddWithValue("p_brsaobracajnedozvole", motocikl.BrSaobracajneDozvole);
                cmd.Parameters.AddWithValue("p_primarnaupotreba", motocikl.PrimarnaUpotreba.ToString());
                cmd.Parameters.AddWithValue("p_korisnikid", motocikl.KorisnikId);

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

        public async Task<bool> AzurirajAsync(Motocikl motocikl)
        {
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return false;

            try
            {
                await using var cmd = new NpgsqlCommand("spizmenimotocikl", db.DajKonekciju());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_id", motocikl.Id);
                cmd.Parameters.AddWithValue("p_brendmodel", motocikl.BrendModel);
                cmd.Parameters.AddWithValue("p_godinaproizvodnje", motocikl.GodinaProizvodnje);
                cmd.Parameters.AddWithValue("p_brsaobracajnedozvole", motocikl.BrSaobracajneDozvole);
                cmd.Parameters.AddWithValue("p_primarnaupotreba", motocikl.PrimarnaUpotreba.ToString());

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

        public async Task<bool> ObrisiAsync(Guid id)
        {
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return false;

            try
            {
                await using var cmd = new NpgsqlCommand("spobrisimotocikl", db.DajKonekciju());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_id", id);

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
