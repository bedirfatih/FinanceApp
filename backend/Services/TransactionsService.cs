using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TransactionsService(AppDbContext db)
{
    public Task<List<Transaction>> GetAllAsync() => db.Transactions.ToListAsync();

    public async Task<Transaction?> GetByIdAsync(int id) => await db.Transactions.FindAsync(id);

    public Task<List<Transaction>> GetByUserIdAsync(int userId) =>
        db.Transactions.Where(t => t.UserId == userId).ToListAsync();

    public async Task<Transaction> CreateAsync(CreateTransactionRequest req)
    {
        var transaction = new Transaction
        {
            UserId = req.UserId,
            Amount = req.Amount,
            Category = req.Category,
            Description = req.Description,
            Date = req.Date ?? DateTime.UtcNow
        };
        db.Transactions.Add(transaction);
        await db.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> UpdateAsync(int id, UpdateTransactionRequest req)
    {
        var transaction = await db.Transactions.FindAsync(id);
        if (transaction is null) return null;

        if (req.Amount is not null) transaction.Amount = req.Amount.Value;
        if (req.Category is not null) transaction.Category = req.Category;
        if (req.Description is not null) transaction.Description = req.Description;
        if (req.Date is not null) transaction.Date = req.Date.Value;

        await db.SaveChangesAsync();
        return transaction;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await db.Transactions.FindAsync(id);
        if (transaction is null) return false;

        db.Transactions.Remove(transaction);
        await db.SaveChangesAsync();
        return true;
    }
}
