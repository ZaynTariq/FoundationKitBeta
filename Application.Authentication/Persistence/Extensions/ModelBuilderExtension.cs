namespace FoundationKit.Authentication.Persistence.Extensions;
using FoundationKit.Authentication.Core.Entities.AccountEntities;
using FoundationKit.Authentication.Core.Entities.RoleEntities;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public static class ModelBuilderExtensions
{
    public static void ConfigureIdentityRelations<TKey, TUser, TRole, TPermission, TModuleAction, TModule>(
        this ModelBuilder modelBuilder)
        where TUser : AppUserBase<TKey>
        where TRole : AppRoleBase<TKey>
        where TPermission : AppPermissionBase<TKey>
        where TModuleAction : AppModuleActionBase<TKey>
        where TModule : AppModuleBase<TKey>
    {

        modelBuilder.Entity<TUser>(x =>
        {
            x.HasMany(s => s.Roles)
            .WithMany(x => (IEnumerable<TUser>)x.Users)
            .UsingEntity("UserRoles");


            x.ToTable("Users");
        });


        modelBuilder.Entity<TRole>(x =>
        {
            x.HasMany(s => s.Permissions)
                                     .WithOne(x => (TRole)x.Role)
                                     .HasForeignKey(x => x.RoleId);

            x.ToTable("Roles");

        });


        modelBuilder.Entity<TModule>(x =>
                                      x.HasMany(s => s.ModuleActions)
                                     .WithOne(x => (TModule)x.Module)
                                     .HasForeignKey(x => x.ModuleId));


        modelBuilder.Entity<TModuleAction>(x =>
                                   x.HasOne(s => s.Module)
                                  .WithMany(x => (IEnumerable<TModuleAction>)x.ModuleActions)
                                  .HasForeignKey(x => x.ModuleId));


        modelBuilder.Entity<TPermission>(x =>
                                  x.HasOne(s => s.ModuleAction)
                                 .WithOne(x => (TPermission)x.Permission)
                                 .HasForeignKey<TPermission>(x => x.ModuleActionId));

    }

    public static void ConfigureRelations<TKey, TUser, TRole>(
      this ModelBuilder modelBuilder)
      where TUser : AppUserBase<TKey>
      where TRole : AppRoleBase<TKey>
    {

        modelBuilder.Entity<TUser>(x =>
        {
            x.HasMany(s => s.Roles)
            .WithMany(x => (IEnumerable<TUser>)x.Users)
            .UsingEntity("UserRoles");


            x.ToTable("Users");
        });


        modelBuilder.Entity<TRole>(x =>
        {
            x.HasMany(s => s.Permissions)
                                     .WithOne(x => (TRole)x.Role)
                                     .HasForeignKey(x => x.RoleId);

            x.ToTable("Roles");

        });

    }
}
