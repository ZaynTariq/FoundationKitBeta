namespace FoundationKit.Authentication.Persistence.DataContext;

using FoundationKit.Authentication.Core.Entities.AccountEntities;
using FoundationKit.Authentication.Core.Entities.RoleEntities;
using FoundationKit.Authentication.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

public class DbContextBase<TKey, TUser, TRole>(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
    where TUser : AppUserBase<TKey>
    where TRole : AppRoleBase<TKey>

{
    public DbSet<TUser> Users { get; set; }
    public DbSet<TRole> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureRelations<TKey, TUser, TRole>();
    }

}


public class DbContextBase<TKey, TUser, TRole, TPemission, TModuleAction, TModule> : DbContext
    where TUser : AppUserBase<TKey>
    where TRole : AppRoleBase<TKey>
    where TPemission : AppPermissionBase<TKey>
    where TModuleAction : AppModuleActionBase<TKey>
    where TModule : AppModuleBase<TKey>

{
    public DbContextBase(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {

    }

    public DbSet<TUser> Users { get; set; }
    public DbSet<TRole> Roles { get; set; }
    public DbSet<TPemission> RolePermissions { get; set; }
    public DbSet<TModuleAction> ModuleActions { get; set; }
    public DbSet<TModule> Modules { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureIdentityRelations<TKey, TUser, TRole, TPemission, TModuleAction, TModule>();
    }

}