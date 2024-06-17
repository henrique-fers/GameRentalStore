using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameRentalStore.Controllers.Models;
using GameRentalStore.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GameRentalStore.Services
{
    public class TokenService : ITokenService
    {
        //Corrigir endpoint Token
        public string GenerateToken(Employee employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Role, employee.Role.ToString()),
                    new ("Email",employee.Email),
                }),

                Expires = DateTime.UtcNow.AddMinutes(59),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}