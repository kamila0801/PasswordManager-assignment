namespace PasswordManager_Security.Repository;

public interface IAuthDbSeeder
{
    void SeedDevelopment();
    void SeedProduction();
}