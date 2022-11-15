using JWT_Token_Authentication_And_Authorization.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Token_Authentication_And_Authorization.DAL
{
    public class AccountDAL : IAccountDAL
    {
        private readonly IConfiguration _configuration;

        Dictionary<string, string> userList = new Dictionary<string, string>()
        {
            {"Vimal", "pass@123" },
            {"Test", "pass@321" },
        };

        public AccountDAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JWTTokens AuthenticateAndGenerateToken(Users user)
        {
            if (!userList.Any(x => x.Key == user.UserName && x.Value == user.Password))
            {
                return null;
            }

            // Generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),

                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesc);
            return new JWTTokens { Token = tokenHandler.WriteToken(token) };
        }
    }
}
