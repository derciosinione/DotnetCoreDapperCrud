namespace Application.Contracts.Request;

public record CreateUserRequest(string Name, string Email, DateTime Birthday);