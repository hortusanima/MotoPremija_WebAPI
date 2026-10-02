
using MotoPremija_WebAPI.SlojPodataka.ADO.FunkcijeKonekcije;
using MotoPremija_WebAPI.SlojPodataka.Modeli;
using MotoPremija_WebAPI.SlojPoslovneLogike.Interfejsi.ADO;
using Npgsql;
using System.Data;

namespace MotoPremija_WebAPI.SlojPodataka.ADO.Repozitorijumi
{
    public class KorisnikADORepozitorijum : IKorisnikADORepozitorijum
    {
        private readonly string _konekcioniString;

        public KorisnikADORepozitorijum(string konekcioniString)
        {
            _konekcioniString = konekcioniString;
        }

        public async Task<bool> KreirajAsync(Korisnik korisnik)
        {
            var db = new Konekcija(_konekcioniString);
            if (!await db.OtvoriKonekcijuAsync()) return false;

            var trans = new Transakcija(db);
            await trans.ZapocniTransakcijuAsync();

            try
            {
                await using var cmd = new NpgsqlCommand("spdodajkorisnika", db.DajKonekciju(), trans.DajTransakciju());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("p_ime", korisnik.Ime);
                cmd.Parameters.AddWithValue("p_prezime", korisnik.Prezime);
                cmd.Parameters.AddWithValue("p_jmbg", korisnik.JMBG);
                cmd.Parameters.AddWithValue("p_imejl", korisnik.Imejl);
                cmd.Parameters.AddWithValue("p_adresa", korisnik.Adresa);
                cmd.Parameters.AddWithValue("p_uloga", korisnik.Uloga.ToString());
                cmd.Parameters.AddWithValue("p_telefon", korisnik.Telefon);
                cmd.Parameters.AddWithValue("p_brvozackedozvole", korisnik.BrVozackeDozvole);
                cmd.Parameters.AddWithValue("p_lozinkahash", korisnik.LozinkaHash);
                cmd.Parameters.AddWithValue("p_lozinkasalt", korisnik.LozinkaSalt);

                await cmd.ExecuteNonQueryAsync();
                await trans.ZavrsiTransakcijuAsync(false);
                return true;
            }
            catch
            {
                await trans.ZavrsiTransakcijuAsync(true);
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
                await using var cmd = new NpgsqlCommand("spobrisikorisnika", db.DajKonekciju());
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
