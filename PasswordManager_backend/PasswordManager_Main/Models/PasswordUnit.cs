namespace PasswordManager_Main.Models;

public class PasswordUnit
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? UsernameAuth { get; set; }
    public string Website { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string MasterPassword { get; set; }
    public DateTime CreatedAt { get; set; }
}