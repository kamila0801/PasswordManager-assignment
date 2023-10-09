using PasswordManager_Security.IRepository;
using PasswordManager_Security.IService;
using PasswordManager_Security.Models;

namespace PasswordManager_Security.Service;

public class AuthUserService : IAuthUserService
{
    private readonly IUserRepository _userRepo;

    public AuthUserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }
    
    public AuthUser GetUser(string username)
    {
        return _userRepo.FindUser(username);
    }

    public AuthUser Create(AuthUser authUser)
    {
        return _userRepo.SaveUser(authUser);
    }
}