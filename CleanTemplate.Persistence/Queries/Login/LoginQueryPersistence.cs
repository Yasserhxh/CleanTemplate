using CleanTemplate.Application.Authentication.Queries.Login.Persistence;
using CleanTemplate.Application.Common.Interfaces;
using CleanTemplate.Domain.Context;
using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Persistence.Queries.Login;

public class LoginQueryPersistence : ILoginQueryPersistence
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _dbContext;
    public LoginQueryPersistence(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
    }

    public  User? Login(string email)
    {
        var query = _dbContext.Users.FirstOrDefault(p => p.Email == email);
        return query;
    }
}