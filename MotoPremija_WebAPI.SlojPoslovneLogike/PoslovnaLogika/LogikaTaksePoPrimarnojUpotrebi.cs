
using MotoPremija_WebAPI.SlojPodataka.Modeli.Domeni;

namespace MotoPremija_WebAPI.SlojPoslovneLogike.PoslovnaLogika
{
    public class LogikaTaksePoPrimarnojUpotrebi
    {
        private readonly PrimarnaUpotreba _primarnaUpotreba;
        private readonly Dictionary<PrimarnaUpotreba, double> _takseUpotrebe;
        public LogikaTaksePoPrimarnojUpotrebi(PrimarnaUpotreba primarnaUpotreba)
        {
            _primarnaUpotreba = primarnaUpotreba;
            _takseUpotrebe = new Dictionary<PrimarnaUpotreba, double>
            {
                { PrimarnaUpotreba.LICNA, 0 },
                { PrimarnaUpotreba.POSLOVNA,  3500},
                { PrimarnaUpotreba.DOSTAVLJACKA, 2000}
            };

        }

        public double VratiTaksuUpotrebe()
        {
            return _takseUpotrebe[_primarnaUpotreba];
        }
    }
}
