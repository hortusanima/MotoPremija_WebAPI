
using MotoPremija_WebAPI.SlojPodataka.ADO.FunkcijeKonekcije;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using Npgsql;
using System.Data;

namespace MotoPremija_WebAPI.SlojPodataka.ADO.Repozitorijumi
{
    public class OsiguranjeADORepozitorijum : IOsiguranjeADORepozitorijum
    {
        private readonly string _konekcioniString;

        public OsiguranjeADORepozitorijum(string konekcioniString)
        {
            _konekcioniString = konekcioniString;
        }

        public async Task<Osiguranje?> VratiPoIdAsync(Guid id)
        {
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return null;

            try
            {
                await using var cmd = new NpgsqlCommand("SELECT * FROM spdajosiguranjepoid(@p_osiguranjeid)", db.DajKonekciju());
                cmd.Parameters.AddWithValue("p_osiguranjeid", id);

                await using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new Osiguranje
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("id")),
                        BrPolise = reader.GetString(reader.GetOrdinal("brpolise")),
                        GodisnjaPremija = reader.GetDouble(reader.GetOrdinal("godisnjapremija")),
                        MotociklId = reader.GetGuid(reader.GetOrdinal("motociklid")),
                        TipOsiguranjaId = reader.GetGuid(reader.GetOrdinal("tiposiguranjaid")),
                        TipOsiguranja = new TipOsiguranja
                        {
                            Id = reader.GetGuid(reader.GetOrdinal("tiposiguranjaid")),
                            NazivOsiguranja = reader.GetString(reader.GetOrdinal("nazivosiguranja"))
                        }
                    };
                }
                return null;
            }
            finally
            {
                await db.ZatvoriKonekcijuAsync();
            }
        }
        public async Task<List<Osiguranje>> VratiSvePoMotociklIdAsync(Guid motociklId)
        {
            var lista = new List<Osiguranje>();
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return lista;

            try
            {
                await using var cmd = new NpgsqlCommand("SELECT * FROM spdajpolisezamotocikl(@p_motociklid)", db.DajKonekciju());
                cmd.Parameters.AddWithValue("p_motociklid", motociklId);

                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var tipOsiguranjaId = reader.GetGuid(reader.GetOrdinal("tiposiguranjaid"));

                    lista.Add(new Osiguranje
                    {
                        BrPolise = reader.GetString(reader.GetOrdinal("brpolise")),
                        GodisnjaPremija = Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("godisnjapremija"))),
                        MotociklId = motociklId,
                        TipOsiguranjaId = tipOsiguranjaId,
                        TipOsiguranja = new TipOsiguranja
                        {
                            Id = tipOsiguranjaId,
                            NazivOsiguranja = reader.GetString(reader.GetOrdinal("nazivosiguranja"))
                        }
                    });
                }
                return lista;
            }
            finally
            {
                await db.ZatvoriKonekcijuAsync();
            }
        }

        public async Task<bool> KreirajAsync(Osiguranje osiguranje)
        {
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return false;

            try
            {
                await using var cmd = new NpgsqlCommand("CALL spdodajosiguranje(@p_brpolise, @p_godisnjapremija, @p_motociklid, @p_tiposiguranjaid)", db.DajKonekciju());

                cmd.Parameters.AddWithValue("p_brpolise", osiguranje.BrPolise);
                cmd.Parameters.AddWithValue("p_godisnjapremija", Convert.ToDecimal(osiguranje.GodisnjaPremija));
                cmd.Parameters.AddWithValue("p_motociklid", osiguranje.MotociklId);
                cmd.Parameters.AddWithValue("p_tiposiguranjaid", osiguranje.TipOsiguranjaId);

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
                await using var cmd = new NpgsqlCommand("spobrisiosiguranje", db.DajKonekciju());
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
