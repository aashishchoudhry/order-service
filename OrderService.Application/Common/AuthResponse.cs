namespace OrderService.Application.Common;

public record AuthResponse(bool Success, string? Token = null, string? Error = null);
