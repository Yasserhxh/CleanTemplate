using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Infrastructure.Presistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> Users = new();
        public void Add(User user)
        {
            Users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return Users.SingleOrDefault(u => u.Email == email);
        }
    }
}
