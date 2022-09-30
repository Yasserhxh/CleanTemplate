using CleanTemplate.Application.Authentication.Command.Register.Persistence;
using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Domain.Context;
using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Persistence.Commands.Register;

public class RegisterCommandPersistence : IRegisterCommandPersisetence
{ 
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _dbContext;

    public RegisterCommandPersistence(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
    }

    public User? GetUserByEmail(string Email)
    {
        var query = _dbContext.Users.FirstOrDefault(u=>u.Email == Email);
        return query;
    }

    public async Task<User?> Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
        var confirm = await _unitOfWork.Complete();
        return confirm > 0 ? user : null;
    }
}