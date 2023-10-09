namespace PasswordManager_Main.IService;

public interface IEncryptionService
{
    string Encrypt(string plainText, string masterPassword);
    string Decrypt(string cipherText, string masterPassword);
}