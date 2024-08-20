namespace FoundationKit.Authentication.Core.Entities.AccountEntities;

using FoundationKit.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class AppUserBase<TId> : BaseEntity<TId>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime DateOfBirth { get; set; } = default!;
    public string ProfilePictureUrl { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Username { get; set; } = default!;
    public IEnumerable<AppRoleBase<TId>> Roles { get; set; } = [];
}
