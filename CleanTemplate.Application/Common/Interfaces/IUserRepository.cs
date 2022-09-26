using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Common.Interfaces
{
    public interface IUserRepository 
    {
        void Add(User user);
        User? GetUserByEmail(string email);
    }
}
