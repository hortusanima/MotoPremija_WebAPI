
namespace MotoPremija_WebAPI.SlojPoslovneLogike.PoslovnaLogika
{
    public class LogikaGodisnjePremije
    {
        private readonly double _mesecnaNetoPremija;
        private readonly double _faktorStarosti;
        private readonly double _taksaUpotrebe;
        private readonly double _mesecnaTaksa;
        private readonly double _agentskaKomisija;
        public LogikaGodisnjePremije(
            double mesecnaNetoPremija, 
            double faktorStarosti, 
            double taksaUpotrebe
        )
        {
            _mesecnaNetoPremija = mesecnaNetoPremija;
            _faktorStarosti = faktorStarosti;
            _taksaUpotrebe = taksaUpotrebe;
            _mesecnaTaksa = 200;
            _agentskaKomisija = 1.1;
        }

        public double IzracunajGodisnjuPremiju()
        {
            return (_mesecnaNetoPremija * _faktorStarosti + _mesecnaTaksa) * 12 * _agentskaKomisija + _taksaUpotrebe;
        }
    }
}
