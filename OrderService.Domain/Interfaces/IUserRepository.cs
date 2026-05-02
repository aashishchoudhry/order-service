using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User> CreateAsync(User user);
}