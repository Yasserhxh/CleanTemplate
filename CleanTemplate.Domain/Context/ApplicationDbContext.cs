using CleanTemplate.Domain.Common.Errors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CleanTemplate.Domain.Entities;
namespace CleanTemplate.Domain.Context;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

}