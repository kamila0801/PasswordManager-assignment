using PasswordManager_Main.Models;
using PasswordManager_Main.Repository;

namespace PasswordManager_Main.Transformer;

public static class PasswordTransformer
{
    public static PasswordEntity ToPasswordEntity(PasswordUnit unit)
    {
        return new PasswordEntity
        {
            Id = unit.Id,
            UserId = unit.UserId,
            Website = unit.Website,
            Username = unit.Username,
            Password = unit.Password,
            CreatedAt = unit.CreatedAt
        };
    }

    public static PasswordUnit ToPasswordUnit(PasswordEntity entity)
    {
        return new PasswordUnit
        {
            Id = entity.Id,
            UserId = entity.UserId,
            UsernameAuth = null, // Since there's no equivalent property in PasswordEntity, set it to null.
            Website = entity.Website,
            Username = entity.Username,
            Password = entity.Password,
            MasterPassword = "", // Set this property to an empty string or another default value
            CreatedAt = entity.CreatedAt
        };
    }
}



