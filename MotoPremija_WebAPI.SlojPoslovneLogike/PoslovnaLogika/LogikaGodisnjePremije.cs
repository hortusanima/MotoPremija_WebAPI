
using System.Xml.Linq;

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
            string putanjaDoDatoteke = Path.Combine(AppContext.BaseDirectory, "PoslovnaLogika", "PoslovniParametri.xml");
            XDocument xmlDok = XDocument.Load(putanjaDoDatoteke);
            _mesecnaNetoPremija = mesecnaNetoPremija;
            _faktorStarosti = faktorStarosti;
            _taksaUpotrebe = taksaUpotrebe;
            _mesecnaTaksa = double.Parse(xmlDok.Root.Element("MesecnaTaksa").Value);
            _agentskaKomisija = double.Parse(xmlDok.Root.Element("AgentskaKomisija").Value);
        }

        public double IzracunajGodisnjuPremiju()
        {
            return (_mesecnaNetoPremija * _faktorStarosti + _mesecnaTaksa) * 12 * _agentskaKomisija + _taksaUpotrebe;
        }
    }
}
