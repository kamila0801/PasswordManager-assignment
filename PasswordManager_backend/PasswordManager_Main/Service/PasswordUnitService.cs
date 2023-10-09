using PasswordManager_Main.IRepository;
using PasswordManager_Main.IService;
using PasswordManager_Main.Models;
using PasswordManager_Main.Repository;
using PasswordManager_Security.IService;
using PasswordManager_Security.Service;

namespace PasswordManager_Main.Service;

public class PasswordUnitService : IPasswordUnitService
{
    private readonly MainDbContext _context;
    private IEncryptionService _encryptionService;
    private IAuthUserService _userService;
    private IUnitRepository _passwordRepository;

    public PasswordUnitService(MainDbContext context, IAuthUserService userService, IUnitRepository passwordRepository)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userService = userService;
        _encryptionService = new EncryptionService();
        _passwordRepository = passwordRepository;
    }

    public void AddPasswordUnit(PasswordUnit passwordUnit)
        {
            try
            {
                var user = _userService.GetUser(passwordUnit.UsernameAuth);
                passwordUnit.UserId = user.Id;
                passwordUnit.Password = _encryptionService.Encrypt(passwordUnit.Password, passwordUnit.MasterPassword);
                passwordUnit.Username = _encryptionService.Encrypt(passwordUnit.Username, passwordUnit.MasterPassword);
                _passwordRepository.AddPasswordUnit(passwordUnit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw new Exception("Failed to add PasswordUnit", ex);
            }
        }

        public PasswordUnit GetPasswordUnitById(int id, string masterPassword)
        {
            try
            {
                var passwordUnit = _passwordRepository.GetPasswordUnitById(id);
                passwordUnit.Password = _encryptionService.Decrypt(passwordUnit.Password, masterPassword);
                passwordUnit.Username = _encryptionService.Decrypt(passwordUnit.Username, masterPassword);
                return passwordUnit;
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw new Exception("Failed to retrieve PasswordUnit by id", ex);
            }
        }

        public IEnumerable<PasswordUnit> GetAllPasswordUnits(string username, string masterPassword)
        {
            try
            {
                var user = _userService.GetUser(username);

                
                var passwordUnits = _passwordRepository.GetAllPasswordUnits(user.Id);;
                foreach (var passwordUnit in passwordUnits)
                {
                    passwordUnit.Password = _encryptionService.Decrypt(passwordUnit.Password, masterPassword);
                    passwordUnit.Username = _encryptionService.Decrypt(passwordUnit.Username, masterPassword);
                }
                return passwordUnits;
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw new Exception("Failed to retrieve all PasswordUnits", ex);
            }
        }

        public void UpdatePasswordUnit(PasswordUnit passwordUnit, string masterPassword)
        {
            try
            {
                passwordUnit.Password = _encryptionService.Encrypt(passwordUnit.Password, masterPassword);
                passwordUnit.Username = _encryptionService.Encrypt(passwordUnit.Username, masterPassword);
               _passwordRepository.UpdatePasswordUnit(passwordUnit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw new Exception("Failed to update PasswordUnit", ex);
            }
        }

        public void DeletePasswordUnit(PasswordUnit passwordUnit)
        {
            try
            {
                var user = _userService.GetUser(passwordUnit.UsernameAuth);
                passwordUnit.UserId = user.Id;
                _passwordRepository.DeletePasswordUnit(passwordUnit);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                throw new Exception("Failed to delete PasswordUnit", ex);
            }
        }
}
