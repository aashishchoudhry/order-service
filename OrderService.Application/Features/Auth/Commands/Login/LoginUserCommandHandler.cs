using MediatR;
using OrderService.Application.Auth;
using OrderService.Domain.Interfaces;

namespace OrderService.Application.Features.Auth.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            return new AuthResponse(false, Error: "Invalid credentials");

        var token = _tokenGenerator.GenerateToken(user.Username, user.Role);
        return new AuthResponse(true, token);
    }
}
