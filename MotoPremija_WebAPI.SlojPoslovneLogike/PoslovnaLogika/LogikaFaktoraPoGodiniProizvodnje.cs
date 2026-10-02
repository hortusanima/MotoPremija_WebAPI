
namespace MotoPremija_WebAPI.SlojPoslovneLogike.PoslovnaLogika
{
    public class LogikaFaktoraPoGodiniProizvodnje
    {
        private readonly int _godina;
        private readonly Dictionary<string, double> _faktoriStarosti;
        public LogikaFaktoraPoGodiniProizvodnje(int godina)
        {
            _godina = godina;
            _faktoriStarosti = new Dictionary<string, double> 
            {
                { "STAR", 2 },
                { "SREDNJI", 1.6 },
                { "MLAD", 1.2},
                { "NOV", 1 }
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
