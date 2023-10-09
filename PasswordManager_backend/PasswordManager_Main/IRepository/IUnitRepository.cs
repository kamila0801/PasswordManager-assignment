using PasswordManager_Main.Models;

namespace PasswordManager_Main.IRepository;

public interface IUnitRepository
{
    // Create
    void AddPasswordUnit(PasswordUnit passwordUnit);

    // Read
    PasswordUnit GetPasswordUnitById(int id);
    IEnumerable<PasswordUnit> GetAllPasswordUnits(int userId);

    // Update
    void UpdatePasswordUnit(PasswordUnit passwordUnit);

    // Delete
    void DeletePasswordUnit(PasswordUnit passwordUnit);
}