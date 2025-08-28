using Avanade.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Avanade.DTOs.DadosLogin;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Avanade.DTOs.DadosLogin

{
    public class TokenService
    {
        public string GenerateToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Usuario),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
