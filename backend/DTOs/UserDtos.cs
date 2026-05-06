namespace backend.DTOs;

public record CreateUserRequest(string Name, string Email, string Password);
public record UpdateUserRequest(string? Name, string? Email, string? Password, decimal? Balance);
