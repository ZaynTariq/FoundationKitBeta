namespace FoundationKit.Authentication.Persistence.DataAccess.WriteRepositories;

using FoundationKit.Authentication.Persistence.DataAccess.Base;
using Microsoft.EntityFrameworkCore;

public class WriteRepository(DbContext dbContext) : Repository(dbContext), IWriteRepository
{

}
