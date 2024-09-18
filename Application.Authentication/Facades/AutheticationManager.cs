namespace FoundationKit.Authentication.Facades;

using FoundationKit.Authentication.Core.Entities.AccountEntities;
using FoundationKit.Authentication.Persistence.DataAccess.ReadRepositories;
using FoundationKit.Authentication.Persistence.DataAccess.WriteRepositories;

public class AuthenticationManager<User, Tkey>
    where User : AppUserBase<Tkey>, new()
{
    private readonly IReadRepository _readRepository;
    private readonly IWriteRepository _writeRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAuditLogger _auditLogger;
    private readonly IRoleManager _roleManager;

    public AuthenticationManager(IReadRepositoryForReadOperation<User> readRepository,
                                 IWriteRepository<User> writeRepository,
                                 IPasswordHasher passwordHasher,
                                 IAuditLogger auditLogger,
                                 IRoleManager roleManager)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _passwordHasher = passwordHasher;
        _auditLogger = auditLogger;
        _roleManager = roleManager;
    }

    public User Login(string username, string password)
    {
        // Example: Fetch user from the repository
        var user = _readRepository.GetByPredicate(u => u.Username == username);

        if (user == null)
        {
            _auditLogger.LogFailedLoginAttempt(username);
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        // Verify password
        if (!_passwordHasher.VerifyPassword(user.Password, password))
        {
            _auditLogger.LogFailedLoginAttempt(username);
            HandleFailedLogin(user);
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        ResetFailedLoginAttempts(user);
        _auditLogger.LogSuccessfulLogin(user.Id);
        return user;
    }

    public void AddUser(User newUser, string role)
    {
        // Check if user already exists
        var existingUser = _readRepository.GetByPredicate(u => u.Username == newUser.Username);

        if (existingUser != null)
        {
            throw new InvalidOperationException("User already exists.");
        }

        // Hash the password before storing it
        newUser.Password = _passwordHasher.HashPassword(newUser.Password);
        SortedSet<int> s = new();
        s.Add(1);

        // Assign role
        if (!_roleManager.RoleExists(role))
        {
            throw new InvalidOperationException("Role does not exist.");
        }
        newUser.Role = role;

        // Add new user
        _writeRepository.Add(newUser);
        _writeRepository.SaveChanges();

        _auditLogger.LogUserAdded(newUser.Id);
    }

    public void DeleteUser(Guid userId)
    {
        // Fetch user from the repository
        var user = _readRepository.GetById(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        // Delete user
        _writeRepository.Delete(user);
        _writeRepository.SaveChanges();

        _auditLogger.LogUserDeleted(userId);
    }

    public void ChangeUserPassword(Guid userId, string newPassword)
    {
        var user = _readRepository.GetById(userId);

        if (user == null)
        {
            throw new InvalidOperationException("Usernot found.");
        }
        // Hash the new password
        user.Password = _passwordHasher.HashPassword(newPassword);

        // Update user in repository
        _writeRepository.Update(user);
        _writeRepository.SaveChanges();

        _auditLogger.LogPasswordChange(userId);
    }

    public IEnumerable<User> GetAllUsers()
    {
        // Get all users from the repository
        return _readRepository.GetAll();
    }

    public void AssignRole(Guid userId, string role)
    {
        var user = _readRepository.GetById(userId);

        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        if (!_roleManager.RoleExists(role))
        {
            throw new InvalidOperationException("Role does not exist.");
        }

        // Assign new role to the user
        user.Role = role;

        // Update user in repository
        _writeRepository.Update(user);
        _writeRepository.SaveChanges();

        _auditLogger.LogRoleAssignment(userId, role);
    }

    private void HandleFailedLogin(User user)
    {
        user.FailedLoginAttempts++;

        if (user.FailedLoginAttempts >= 5)  // Arbitrary lockout threshold
        {
            user.IsLockedOut = true;
            _auditLogger.LogAccountLockout(user.Id);
        }

        _writeRepository.Update(user);
        _writeRepository.SaveChanges();
    }

    private void ResetFailedLoginAttempts(User user)
    {
        user.FailedLoginAttempts = 0;
        user.IsLockedOut = false;

        _writeRepository.Update(user);
        _writeRepository.SaveChanges();
    }
}
