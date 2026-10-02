
using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;
using System.Xml.Linq;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.PoslovnaLogika
{
    public class LogikaTaksePoPrimarnojUpotrebi
    {
        private readonly PrimarnaUpotreba _primarnaUpotreba;
        private readonly Dictionary<PrimarnaUpotreba, double> _takseUpotrebe;
        public LogikaTaksePoPrimarnojUpotrebi(PrimarnaUpotreba primarnaUpotreba)
        {
            string putanjaDoDatoteke = Path.Combine(AppContext.BaseDirectory, "PoslovnaLogika", "PoslovniParametri.xml");
            XDocument xmlDok = XDocument.Load(putanjaDoDatoteke);
            _primarnaUpotreba = primarnaUpotreba;
            _takseUpotrebe = new Dictionary<PrimarnaUpotreba, double>
            {
                { PrimarnaUpotreba.LICNA, double.Parse(xmlDok.Root.Element("TaksaUpotrebeLicna").Value) },
                { PrimarnaUpotreba.POSLOVNA,  double.Parse(xmlDok.Root.Element("TaksaUpotrebePoslovna").Value)},
                { PrimarnaUpotreba.DOSTAVLJACKA, double.Parse(xmlDok.Root.Element("TaksaUpotrebeDostavljacka").Value)}
            };

        }

        public double VratiTaksuUpotrebe()
        {
            return _takseUpotrebe[_primarnaUpotreba];
        }
    }
}
