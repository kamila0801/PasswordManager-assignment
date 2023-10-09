using PasswordManager_Security.Models;

namespace PasswordManager_Security.IService;

public interface IAuthUserService
{
    AuthUser GetUser(string username);
    AuthUser Create(AuthUser authUser);
}