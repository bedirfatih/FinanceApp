namespace backend.DTOs;

public record CreateTransferRequest(int FromUserId, int ToUserId, decimal Amount);
