using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enum;
using HospitalLibrary.Core.Repository.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HospitalLibrary.Core.Repository
{
    public class JwtManagerRepository : IJwtManagerRepository
    {
        private readonly IConfiguration _iconfiguration;

        public JwtManagerRepository(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        public Tokens Authenticate(int userId, string name, Role r)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, r.ToString())
                    //     new Claim("Id", user.Id.ToString()),
                  //  new Claim("Email", user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}