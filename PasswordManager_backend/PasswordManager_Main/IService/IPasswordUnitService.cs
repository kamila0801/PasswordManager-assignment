using PasswordManager_Main.Models;

namespace PasswordManager_Main.IService;

public interface IPasswordUnitService
{
    void AddPasswordUnit(PasswordUnit passwordUnit);
    PasswordUnit GetPasswordUnitById(int id, string masterPassword);
    IEnumerable<PasswordUnit> GetAllPasswordUnits(string username, string masterPassword);
    void UpdatePasswordUnit(PasswordUnit passwordUnit, string masterPassword);
    void DeletePasswordUnit(PasswordUnit passwordUnit);
}