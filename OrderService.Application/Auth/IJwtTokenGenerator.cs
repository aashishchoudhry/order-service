namespace OrderService.Application.Auth;

public interface IJwtTokenGenerator
{
    string GenerateToken(string username, string role);
}
