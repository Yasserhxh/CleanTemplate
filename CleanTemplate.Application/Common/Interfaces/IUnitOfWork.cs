using Microsoft.EntityFrameworkCore.Storage;

namespace CleanTemplate.Application.Common.Interfaces;

public interface IUnitOfWork: IAsyncDisposable
{
    Task<int> Complete();
    IDbContextTransaction BeginTransaction();
}