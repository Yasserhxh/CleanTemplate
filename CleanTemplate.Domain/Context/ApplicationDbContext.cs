using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CleanTemplate.Domain.Entities;
namespace CleanTemplate.Domain.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }
    public static DbSet<User> Users => null!;
}