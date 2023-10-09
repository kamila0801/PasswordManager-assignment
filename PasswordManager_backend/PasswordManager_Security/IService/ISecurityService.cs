using PasswordManager_Security.Models;

namespace PasswordManager_Security.IService;

public interface ISecurityService
{
    JwtToken GenerateJwtToken(string username, string password);
    string HashedPassword(byte[] userSalt, string password);
    AuthUser GenerateNewAuthUser(string username, string password = "password");
    byte[] GenerateSalt();
}