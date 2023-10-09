namespace PasswordManager_Main.Repository;

public interface IMainDbSeeding
{
    void SeedDevelopment();
    void SeedProduction();
}