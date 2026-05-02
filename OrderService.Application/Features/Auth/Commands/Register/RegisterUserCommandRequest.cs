using MediatR;
using OrderService.Application.Common;

namespace OrderService.Application.Features.Auth.Commands.Register;

public record RegisterUserCommandRequest(
    string FirstName,
    string LastName,
    string Username,
    string Password) : IRequest<AuthResponse>;
