namespace FoundationKit.Authentication.Persistence.DataAccess.WriteRepositories;

using FoundationKit.Authentication.Persistence.DataAccess.Base;
using FoundationKit.Shared.Common.ApiResponse;
using Microsoft.EntityFrameworkCore;
using System;

public class WriteRepository(DbContext dbContext) : Repository(dbContext), IWriteRepository
{

    public async Task<ApplicationResult<TEntity>> AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        try
        {
            var response = await DbContext.Set<TEntity>().AddAsync(entity);

            await DbContext.SaveChangesAsync();

            return response.Entity;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public ApplicationResult<TEntity> Attach<TEntity>(TEntity entity)
       where TEntity : class
    {
        try
        {
            var response = DbContext.Set<TEntity>().Attach(entity);

            return response.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ApplicationResult<int>> Delete<TEntity>(TEntity entity) where TEntity : class
    {
        try
        {
            DbContext.Set<TEntity>().Remove(entity);

            var response = await DbContext.SaveChangesAsync();

            return response;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }

    public async Task<ApplicationResult<int>> SaveChangesAsync()
    {
        try
        {
            var response = await DbContext.SaveChangesAsync();

            return response;
        }
        catch (Exception ex)
        {
            return ex;
        }
    }
}
