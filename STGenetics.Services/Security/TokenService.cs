
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using STGenetics.Application.Tools.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace STGenetics.Application.Security
{

    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public bool ValidateUser(User user)
        {
            if (user.Name != user.Password) return false;

            if(!user.Name.Equals(_jwtSettings.User.Name, StringComparison.OrdinalIgnoreCase)) return false;

            if (!user.Password.Equals(_jwtSettings.User.Password, StringComparison.OrdinalIgnoreCase)) return false;

            return true;
        }


        public Task<string> GetToken()
        {
            var secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var securityKey = new SymmetricSecurityKey(secret);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("Version", "1.0"),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, "Cmargok"),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", "1"),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Name, "Cmargok"),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_jwtSettings.DurationInMinutes)),
                signingCredentials: credentials
                );

            var tokenGenerated =new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(tokenGenerated);
        }
    }
}
