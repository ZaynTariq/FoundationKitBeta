namespace FoundationKit.Authentication.Persistence.DataAccess.ReadRepositories;

using FoundationKit.Authentication.Persistence.DataAccess.Base;
using Microsoft.EntityFrameworkCore;

public class ReadRepository(DbContext dbContext) :
    Repository(dbContext), IReadRepository
{

}
