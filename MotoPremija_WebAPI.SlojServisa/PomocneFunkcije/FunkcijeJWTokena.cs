
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MotoPremija_WebAPI.SlojServisa.PomocneFunkcije.PomocniModeli;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MotoPremija_WebAPI.SlojServisa.PomocneMetode
{
    public class FunkcijeJWTokena(IOptions<JWTOpcije> jwtOpcije)
    {
        private readonly JWTOpcije _jwtOpcije =
           jwtOpcije.Value;

        public string GenerisiToken(string id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8
                .GetBytes(_jwtOpcije.TajniKljuc!));
            var credentials = new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(
                id,
                null,
                null,
                null,
                DateTime.UtcNow.AddDays(1)
            );

            var securityToken = new JwtSecurityToken(
                header,
                payload
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(securityToken);
        }
    }
}
