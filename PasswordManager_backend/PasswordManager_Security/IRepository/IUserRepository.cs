using PasswordManager_Security.Models;

namespace PasswordManager_Security.IRepository;

public interface IUserRepository
{
    AuthUser FindUser(string username);
    AuthUser SaveUser(AuthUser authUser);
}