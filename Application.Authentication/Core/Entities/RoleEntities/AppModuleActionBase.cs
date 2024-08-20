namespace FoundationKit.Authentication.Core.Entities.RoleEntities;

using FoundationKit.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ModuleActions")]
public class AppModuleActionBase<TId> : BaseEntity<TId>
{
    public string Name { get; set; } = default!;
    public TId ModuleId { get; set; } = default!;
    public AppModuleBase<TId> Module { get; set; } = default!;
    public AppPermissionBase<TId> Permission { get; set; } = default!;
}
