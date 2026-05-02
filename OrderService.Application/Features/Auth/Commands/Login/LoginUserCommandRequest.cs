using MediatR;

namespace OrderService.Application.Features.Auth.Commands.Login;

public record LoginUserCommandRequest(string Username, string Password) : IRequest<AuthResponse>;
