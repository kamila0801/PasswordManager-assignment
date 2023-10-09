using Microsoft.EntityFrameworkCore;
using PasswordManager_Security.Repository.Entities;

namespace PasswordManager_Security.Repository;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) :base(options)
    {
      
    }

    public DbSet<LoginUserEntity> LoginUsers { get; set; }
    
}