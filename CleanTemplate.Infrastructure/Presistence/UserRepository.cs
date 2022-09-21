using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTemplate.Infrastructure.Presistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> users = new();
        public void Add(User user)
        {
            users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return users.SingleOrDefault(u => u.Email == email);
        }
    }
}
