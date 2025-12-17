using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.Core.Data;
using PersonalFinance.Core.Models;
using PersonalFinance.Api.DTOs.Users;
using System.Security.Cryptography;

namespace PersonalFinance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    // POST: api/users/register
    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(RegisterUserDto dto)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (existingUser != null)
        {
            return BadRequest("Email already exists.");
        }
 // Create password hash & salt
    using var hmac = new HMACSHA512();
    var passwordSalt = hmac.Key;
    var passwordHash = hmac.ComputeHash(
        System.Text.Encoding.UTF8.GetBytes(dto.Password)
    );
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var response = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, response);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetById(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
