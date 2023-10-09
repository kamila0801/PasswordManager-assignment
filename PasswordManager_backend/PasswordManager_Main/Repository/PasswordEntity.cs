namespace PasswordManager_Main.Repository;

public class PasswordEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Website { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
}