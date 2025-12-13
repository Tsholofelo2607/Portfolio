namespace PersonalFinance.Core.Models;

public class User
{
    public int Id { get; set; }                     // Unique ID
    public string Username { get; set; } = "";      // For login
    public string Email { get; set; } = "";         // For communication/login
    public byte[] PasswordHash { get; set; }        // Encrypted password
    public byte[] PasswordSalt { get; set; }        // Adds security to the hash

    // Navigation property
    public List<Transaction> Transactions { get; set; } = new();
}
