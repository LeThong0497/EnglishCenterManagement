using EnglishCenter.Business.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnglisCenter.Business.Services
{
    public class JwtAuthenticationManage : IJwtAuthenticationManage
    {       
        private readonly string key;
        
        public JwtAuthenticationManage(string key)
        {
            this.key = key;          
        }
        public string Authenticate(string userName, string passWord)
        {           

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddMilliseconds(30),
                SigningCredentials =
            new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDecriptor);
          return  tokenHandler.WriteToken(token);

        }
    }
}
