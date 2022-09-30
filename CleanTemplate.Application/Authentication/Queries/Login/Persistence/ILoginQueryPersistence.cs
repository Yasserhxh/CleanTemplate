using CleanTemplate.Domain.Entities;

namespace CleanTemplate.Application.Authentication.Queries.Login.Persistence;

public interface ILoginQueryPersistence
{
    public  User? Login(string email);

}