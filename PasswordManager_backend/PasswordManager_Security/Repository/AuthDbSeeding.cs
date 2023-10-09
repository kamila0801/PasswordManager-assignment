using PasswordManager_Security.IService;

namespace PasswordManager_Security.Repository;

public class AuthDbSeeding : IAuthDbSeeder
{
    private  AuthDbContext _ctx;
    private  ISecurityService _securityService;

    public AuthDbSeeding(AuthDbContext ctx, ISecurityService securityService)
    {
        _ctx = ctx;
        _securityService = securityService;
    }

    public void SeedDevelopment()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
        _securityService.GenerateNewAuthUser("petro");
        _securityService.GenerateNewAuthUser("admin");
        _securityService.GenerateNewAuthUser("customer");
      
    }

    public void SeedProduction()
    {
        _ctx.Database.EnsureCreated();
        _securityService.GenerateNewAuthUser("petro");
        _securityService.GenerateNewAuthUser("admin");
        _securityService.GenerateNewAuthUser("customer");
    }
}