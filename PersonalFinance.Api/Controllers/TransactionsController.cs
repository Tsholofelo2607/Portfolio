using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Core.Data;
using PersonalFinance.Core.Models;
using PersonalFinance.Api.DTOs.Transactions;

namespace PersonalFinance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/transactions
    [HttpPost]
    public async Task<ActionResult<TransactionResponseDto>> Create(
        CreateTransactionDto dto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);
        if (!userExists)
        {
            return BadRequest("User does not exist.");
        }

        var transaction = new Transaction
        {
            Amount = dto.Amount,
            Description = dto.Description,
            Date = dto.Date,
            UserId = dto.UserId,
            CategoryId = dto.CategoryId
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        var response = new TransactionResponseDto
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Description = transaction.Description,
            Date = transaction.Date,
            UserId = transaction.UserId,
            CategoryId = transaction.CategoryId
        };

        return CreatedAtAction(nameof(GetById),
            new { id = transaction.Id }, response);
    }

    // GET: api/transactions/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionResponseDto>> GetById(int id)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound();
        }

        return new TransactionResponseDto
        {
            Id = transaction.Id,
            Amount = transaction.Amount,
            Description = transaction.Description,
            Date = transaction.Date,
            UserId = transaction.UserId,
            CategoryId = transaction.CategoryId
        };
    }
}
