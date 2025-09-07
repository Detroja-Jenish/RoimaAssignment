using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers
{
    public static class JwtHelper
    {
        public static string SignAuthToken(string FirstName, string LastName, string Email, string RoleTitle, IConfiguration _config)
        {
            var jwtKey = _config["Jwt:Key"];
            var jwtIssuer = _config["Jwt:Issuer"];
            var jwtAudience = _config["Jwt:Audience"];
            IEnumerable<Claim> claims;
            claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, FirstName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("FirstName", FirstName.ToString()),
                        new Claim("LastName", LastName.ToString()),
                        new Claim("Email", Email.ToString()),
                        new Claim("RoleTitle", RoleTitle.ToString())
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}