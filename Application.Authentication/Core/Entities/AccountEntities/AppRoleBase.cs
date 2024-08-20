namespace FoundationKit.Authentication.Core.Entities.AccountEntities;

using FoundationKit.Authentication.Core.Entities.RoleEntities;
using FoundationKit.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Roles")]
public class AppRoleBase<TId> : BaseEntity<TId>
{
    public string Name { get; set; } = default!;
    public IEnumerable<AppUserBase<TId>> Users { get; set; } = [];
    public IEnumerable<AppPermissionBase<TId>> Permissions { get; set; } = [];
}
