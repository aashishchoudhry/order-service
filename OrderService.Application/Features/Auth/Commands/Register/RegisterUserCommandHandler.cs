using MediatR;
using OrderService.Application.Auth;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Auth.Commands.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
    {
        var existing = await _userRepository.GetByUsernameAsync(request.Username);
        if (existing != null)
            return new AuthResponse(false, Error: "Username already exists");

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            Role = "User"
        };

        await _userRepository.CreateAsync(user);
        return new AuthResponse(true);
    }
}
