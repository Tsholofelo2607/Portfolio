namespace PersonalFinance.Core.Models;

public class Category
{
    public int Id { get; set; }                 // Unique ID
    public string Name { get; set; } = "";      // e.g. Food, Rent, Transport, Salary

    // Navigation property
    public List<Transaction> Transactions { get; set; } = new();
}
