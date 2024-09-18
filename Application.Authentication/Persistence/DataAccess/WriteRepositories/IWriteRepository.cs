namespace FoundationKit.Authentication.Persistence.DataAccess.WriteRepositories;

using FoundationKit.Authentication.Persistence.DataAccess.Base;
using FoundationKit.Shared.Common.ApiResponse;

public interface IWriteRepository : IRepository
{
    Task<ApplicationResult<TEntity>> AddAsync<TEntity>(TEntity entity) where TEntity : class;
    ApplicationResult<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;
    Task<ApplicationResult<int>> Delete<TEntity>(TEntity entity) where TEntity : class;
    Task<ApplicationResult<int>> SaveChangesAsync();
}
