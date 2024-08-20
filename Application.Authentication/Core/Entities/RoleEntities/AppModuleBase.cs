namespace FoundationKit.Authentication.Core.Entities.RoleEntities;

using FoundationKit.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Modules")]
public class AppModuleBase<TId> : BaseEntity<TId>
{
    public string ModuleName { get; set; } = default!;
    public IEnumerable<AppModuleActionBase<TId>> ModuleActions { get; set; } = [];
}
