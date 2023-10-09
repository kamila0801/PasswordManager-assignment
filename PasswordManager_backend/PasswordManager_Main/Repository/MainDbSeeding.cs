using PasswordManager_Main.IService;
using PasswordManager_Main.Models;

namespace PasswordManager_Main.Repository;

public class MainDbSeeding: IMainDbSeeding
{
    private readonly MainDbContext _ctx;
    private readonly IPasswordUnitService _passwordunitService;

    public MainDbSeeding(MainDbContext ctx, IPasswordUnitService passwordService)
    {
        _ctx = ctx;
        _passwordunitService = passwordService;

    }

    public void SeedDevelopment()
    {
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        var pass1 = new PasswordUnit
        {
            UserId = 1,
            Website = "example.com",
            UsernameAuth = "petro",
            Username = "user1",
            Password = "password1",
            MasterPassword = "password",
            CreatedAt = DateTime.Now
        };

        var pass2 = new PasswordUnit
        {
            UserId = 2,
            Website = "test.com",
            UsernameAuth = "admin",
            Username = "user2",
            Password = "password2",
            MasterPassword = "password",
            CreatedAt = DateTime.Now
        };
        
        _passwordunitService.AddPasswordUnit(pass1);
        _passwordunitService.AddPasswordUnit(pass2);
    }

    public void SeedProduction()
    {
        // Seed production data if needed
    }
}