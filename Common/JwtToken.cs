using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common
{
    public class JwtToken
    {
        public static string GetToken(string username, string role, IConfiguration configuration)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.Role,role)
            };
            var secret = configuration.GetSection("Token")["secret"];
            var issuer = configuration.GetSection("Token")["issuer"];
            var audience = configuration.GetSection("Token")["audience"];
            var expires = int.Parse(configuration.GetSection("Token")["expires"]);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            
            //创建令牌
            var jwt = new JwtSecurityToken(
              issuer: issuer,
              audience: audience,
              claims: claims,
              notBefore: DateTime.Now,
              expires: DateTime.Now.AddMinutes(expires),
              signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
