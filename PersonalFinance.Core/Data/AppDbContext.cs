using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PersonalFinance.Core.Models;

namespace PersonalFinance.Core.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Budget> Budgets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User → Transaction (1-to-many)
        modelBuilder.Entity<Transaction>()
           .HasOne(t => t.User)
        .WithMany(u => u.Transactions)
        .HasForeignKey(t => t.UserId);

        // User → Budget (1-to-many)
        modelBuilder.Entity<Budget>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Food" },
        new Category { Id = 2, Name = "Rent" },
        new Category { Id = 3, Name = "Transport" },
        new Category { Id = 4, Name = "Salary" },
        new Category { Id = 5, Name = "Entertainment" });
    }
}

// Design-time factory for migrations
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        // Use your actual database provider here, e.g., Sqlite, SQL Server, etc.
        optionsBuilder.UseSqlite("Data Source=personalfinance.db");

        return new AppDbContext(optionsBuilder.Options);
    }
}

