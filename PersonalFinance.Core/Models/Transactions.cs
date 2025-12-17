namespace PersonalFinance.Core.Models;

public class Transaction
{
    public int Id { get; set; }           // Primary key
    public decimal Amount { get; set; }   // Money spent or received
    public DateTime Date { get; set; }    // Date of the transaction
    public string Description { get; set; } = string.Empty; // Optional text

    public string Category { get; set; } = string.Empty;    // e.g. Food, Rent, Transport
      public int CategoryId { get; set; }

    public bool IsExpense { get; set; }   // true = expense, false = income
     public int UserId { get; set; }
    public User User { get; set; } = null!;


}
