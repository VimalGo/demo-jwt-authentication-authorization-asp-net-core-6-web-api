using JWT_Token_Authentication_And_Authorization.Models;

namespace JWT_Token_Authentication_And_Authorization.DAL
{
    public interface IAccountDAL
    {
        JWTTokens AuthenticateAndGenerateToken(Users user);
    }
}
