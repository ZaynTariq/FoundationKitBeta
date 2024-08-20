namespace FoundationKit.Authentication.Core.Entities.RoleEntities;

using FoundationKit.Authentication.Core.Entities.AccountEntities;
using FoundationKit.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("RolePermissions")]
public class AppPermissionBase<TId> : BaseEntity<TId>
{
    public AppRoleBase<TId> Role { get; set; } = default!;
    public TId RoleId { get; set; } = default!;
    public AppModuleActionBase<TId> ModuleAction { get; set; } = default!;
    public TId ModuleActionId { get; set; } = default!;

}
