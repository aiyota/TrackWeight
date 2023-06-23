using TrackWeight.Api.Models;

namespace TrackWeight.Api.Persistence;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task<User?> GetByEmailAsync(string email);

    Task<User> CreateUserAsync(
        string firstName,
        string lastName,
        string email,
        string passwordHash);

    Task<User> UpdateAsync(
        Guid userId,
        string? firstName,
        string? lastName,
        string? email,
        string? passwordHash);
}
