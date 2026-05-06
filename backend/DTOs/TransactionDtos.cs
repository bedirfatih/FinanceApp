namespace backend.DTOs;

public record CreateTransactionRequest(int UserId, decimal Amount, string Category, string Description, DateTime? Date);
public record UpdateTransactionRequest(decimal? Amount, string? Category, string? Description, DateTime? Date);
