namespace FoundationKit.Web.Entities;

using FoundationKit.Authentication.Core.Entities.AccountEntities;

public class AppUser : AppUserBase<long>
{
    // Additional properties for the user can be added here
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class AppRole : AppRoleBase<long>
{
    // Additional properties for the role can be added here
    public string Description { get; set; }
}