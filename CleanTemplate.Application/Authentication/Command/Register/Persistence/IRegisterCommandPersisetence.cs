using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Authentication.Command.Register.Persistence;

public interface IRegisterCommandPersisetence
{
    public User? GetUserByEmail(string Email);

    public Task<User?> Add(User user);
}