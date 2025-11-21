

public interface IAuthService
{
    Task<AuthModel> RegisterAsync(User user, string password);
    Task<AuthModel> LoginAsync(User user, string password);

    Task<User?> GetCurrentUserAsync();
}