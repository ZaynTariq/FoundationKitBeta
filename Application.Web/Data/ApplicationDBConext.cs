namespace FoundationKit.Web.Data;

using FoundationKit.Authentication.Persistence.DataContext;
using FoundationKit.Web.Entities;
using Microsoft.EntityFrameworkCore;

public class ApplicationDBConext : DbContextBase<long, AppUser, AppRole>
{
    public ApplicationDBConext(DbContextOptions<ApplicationDBConext> dbContextOptions)
        : base(dbContextOptions)
    {
    }
}
