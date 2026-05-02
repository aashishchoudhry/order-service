namespace OrderService.API.Contracts;

public record LoginRequest(string Username, string Password);

public record RegisterRequest(string FirstName, string LastName, string Username, string Password);
