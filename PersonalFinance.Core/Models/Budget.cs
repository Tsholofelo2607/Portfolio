namespace PersonalFinance.Core.Models;

public class Budget
{
    public int Id { get; set; }                     // Unique ID
    public decimal Amount { get; set; }             // Budget amount (e.g., R500 for Food)
    public string Category { get; set; } = "";      // Category the budget applies to
    public int Month { get; set; }                  // 1 = Jan, 2 = Feb, etc.
    public int Year { get; set; }                   // e.g., 2025

    // User relationship
    public int UserId { get; set; }                 // Who owns this budget
    public User User { get; set; } = null!;
}
