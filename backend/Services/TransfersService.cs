using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class TransfersService(AppDbContext db)
{
    public Task<List<Transfer>> GetAllAsync() => db.Transfers.ToListAsync();

    public async Task<Transfer?> GetByIdAsync(int id) => await db.Transfers.FindAsync(id);

    public async Task<Transfer> CreateAsync(CreateTransferRequest req)
    {
        var fromUser = await db.Users.FindAsync(req.FromUserId);
        var toUser = await db.Users.FindAsync(req.ToUserId);

        var transfer = new Transfer
        {
            FromUserId = req.FromUserId,
            ToUserId = req.ToUserId,
            Amount = req.Amount,
            Status = "Pending"
        };

        if (fromUser is null || toUser is null || fromUser.Balance < req.Amount)
        {
            transfer.Status = "Failed";
        }
        else
        {
            fromUser.Balance -= req.Amount;
            toUser.Balance += req.Amount;
            transfer.Status = "Completed";
        }

        db.Transfers.Add(transfer);
        await db.SaveChangesAsync();
        return transfer;
    }
}
