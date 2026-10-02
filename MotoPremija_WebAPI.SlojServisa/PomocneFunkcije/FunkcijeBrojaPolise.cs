
using System.Security.Cryptography;

namespace MotoPremija_WebAPI.SlojServisa.PomocneFunkcije
{
    public static class FunkcijeBrojaPolise
    {
        private const string alfabet =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_";
        public static string GenerisiBrojPolise()
        {
            Span<byte> bajtovi = stackalloc byte[9];
            RandomNumberGenerator.Fill(bajtovi);

            Span<char> karakteri = stackalloc char[9];
            for (int i = 0; i < 9; i++)
                karakteri[i] = alfabet[bajtovi[i] % alfabet.Length];

            return new string(karakteri);
        }
    }
}
