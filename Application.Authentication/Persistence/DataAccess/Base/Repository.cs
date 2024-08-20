namespace FoundationKit.Authentication.Persistence.DataAccess.Base;

using Microsoft.EntityFrameworkCore;

public class Repository(DbContext dbContext) : IRepository
{
    public DbContext DbContext { get; set; } = dbContext;


}
