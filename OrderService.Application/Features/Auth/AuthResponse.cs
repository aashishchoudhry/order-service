namespace OrderService.Application.Features.Auth;

public record AuthResponse(bool Success, string? Token = null, string? Error = null);
