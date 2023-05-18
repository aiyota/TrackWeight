using TrackWeight.Api.Models;
using TrackWeight.Api.Persistence;
using BC = BCrypt.Net.BCrypt;

namespace TrackWeight.Api.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
        {
            throw new Exception("User is not found");
        }

        return user;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user is null)
        {
            throw new Exception("User is not found");
        }

        return VerifyPassword(password, user.PasswordHash);
    }

    public async Task<User> RegisterAsync(
        string firstName,
        string lastName,
        string email,
        string password)
    {
        return await _userRepository.CreateUserAsync(
            firstName,
            lastName,
            email,
            HashPassword(password));
    }

    public async Task<User> UpdateUserAsync(
        Guid userId,
        string? userName,
        string? firstName,
        string? lastName,
        string? password)
    {
        var passwordHash = string.IsNullOrEmpty(password) 
                            ? null 
                            : HashPassword(password);
        return await _userRepository.UpdateAsync(
            userId,
            userName,
            firstName,
            lastName,
            passwordHash);
    }

    public static string HashPassword(string password)
    {
        string salt = BC.GenerateSalt();
        string hashedPassword = BC.HashPassword(password, salt);
        return hashedPassword;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        return BC.Verify(password, hashedPassword);
    }
}
