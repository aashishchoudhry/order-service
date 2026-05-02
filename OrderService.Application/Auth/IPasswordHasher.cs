namespace OrderService.Application.Auth;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool Verify(string password, string passwordHash);
}
