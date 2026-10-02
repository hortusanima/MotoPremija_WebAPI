
using System.Xml.Linq;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.PoslovnaLogika
{
    public class LogikaFaktoraPoGodiniProizvodnje
    {
        private readonly int _godina;
        private readonly Dictionary<string, double> _faktoriStarosti;
        public LogikaFaktoraPoGodiniProizvodnje(int godina)
        {
            string putanjaDoDatoteke = Path.Combine(AppContext.BaseDirectory, "PoslovnaLogika", "PoslovniParametri.xml");
            XDocument xmlDok = XDocument.Load(putanjaDoDatoteke);
            _godina = godina;
            _faktoriStarosti = new Dictionary<string, double> 
            {
                { "STAR", double.Parse(xmlDok.Root.Element("FaktorStarostiStar").Value) },
                { "SREDNJI", double.Parse(xmlDok.Root.Element("FaktorStarostiSrednji").Value) },
                { "MLAD", double.Parse(xmlDok.Root.Element("FaktorStarostiMlad").Value)},
                { "NOV", double.Parse(xmlDok.Root.Element("FaktorStarostiNov").Value) }
            };

        }

        public double VratiFaktorStarosti()
        {
            if(_godina > 2024)
            {
                return _faktoriStarosti["NOV"];
            }
            else if (_godina <= 2024 && _godina > 2014)
            {
                return _faktoriStarosti["MLAD"];
            }
            else if(_godina <= 2014 && _godina > 2004)
            {
                return _faktoriStarosti["SREDNJI"];
            }
            else
            {
                return _faktoriStarosti["STAR"];
            }
        }
    }
}
