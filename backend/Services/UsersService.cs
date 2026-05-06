using System.Security.Cryptography;
using System.Text;
using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class UsersService(AppDbContext db)
{
    public Task<List<User>> GetAllAsync() => db.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(int id) => await db.Users.FindAsync(id);

    public async Task<User> CreateAsync(CreateUserRequest req)
    {
        var user = new User
        {
            Name = req.Name,
            Email = req.Email,
            PasswordHash = HashPassword(req.Password),
            Balance = 0
        };
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(int id, UpdateUserRequest req)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return null;

        if (req.Name is not null) user.Name = req.Name;
        if (req.Email is not null) user.Email = req.Email;
        if (req.Password is not null) user.PasswordHash = HashPassword(req.Password);
        if (req.Balance is not null) user.Balance = req.Balance.Value;

        await db.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return false;

        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return true;
    }

    private static string HashPassword(string password) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}
