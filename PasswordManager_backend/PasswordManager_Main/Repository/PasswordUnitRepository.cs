using PasswordManager_Main.IRepository;
using PasswordManager_Main.Models;
using PasswordManager_Main.Transformer;

namespace PasswordManager_Main.Repository;

public class PasswordUnitRepository : IUnitRepository
{
    private readonly MainDbContext _context;

    public PasswordUnitRepository(MainDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddPasswordUnit(PasswordUnit passwordUnit)
    {
        try
        {
            var passwordEntity = PasswordTransformer.ToPasswordEntity(passwordUnit);
            
            _context.PasswordEntities.Add(passwordEntity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Log or handle the exception here
            throw new Exception("Failed to add PasswordUnit", ex);
        }
    }

    public PasswordUnit GetPasswordUnitById(int id)
    {
        try
        {
            var passwordEntity = _context.PasswordEntities.Find(id);

            // Check if a PasswordEntity was found, and return null if not found
            if (passwordEntity == null)
            {
                return null;
            }

            // Transform the PasswordEntity object to a PasswordUnit object using the transformer
            var passwordUnit = PasswordTransformer.ToPasswordUnit(passwordEntity);

            return passwordUnit;
        }
        catch (Exception ex)
        {
            // Log or handle the exception here
            throw new Exception("Failed to retrieve PasswordUnit by id", ex);
        }
    }

    public IEnumerable<PasswordUnit> GetAllPasswordUnits(int id)
    {
        try
        {
            var passwordEntities = _context.PasswordEntities.Where(p => p.UserId == id).ToList();

            // Create a new list to store the transformed PasswordUnit objects
            var passwordUnits = new List<PasswordUnit>();

            // Loop through the passwordEntities list and transform each item
            foreach (var entity in passwordEntities)
            {
                // Transform the PasswordEntity object to a PasswordUnit object using the transformer
                var passwordUnit = PasswordTransformer.ToPasswordUnit(entity);

                // Add the transformed PasswordUnit object to the passwordUnits list
                passwordUnits.Add(passwordUnit);
            }

            return passwordUnits;
        }
        catch (Exception ex)
        {
            // Log or handle the exception here
            throw new Exception("Failed to retrieve all PasswordUnits", ex);
        }
    }


    public void UpdatePasswordUnit(PasswordUnit passwordUnit)
    {
        try
        {
            var passwordEntity = PasswordTransformer.ToPasswordEntity(passwordUnit);

            // Update the PasswordEntity in the context
            _context.PasswordEntities.Update(passwordEntity);
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
            // var passwordEntity = _context.PasswordEntities.Find(passwordUnit.Id);
            var passwordEntity = _context.PasswordEntities.FirstOrDefault(pe => pe.UserId == passwordUnit.UserId && pe.Id == passwordUnit.Id);

            // Check if a PasswordEntity was found, and do nothing if not found
            if (passwordEntity == null)
            {
                return;
            }

            // Remove the PasswordEntity from the context
            _context.PasswordEntities.Remove(passwordEntity);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Log or handle the exception here
            throw new Exception("Failed to delete PasswordUnit", ex);
        }
    }
}
