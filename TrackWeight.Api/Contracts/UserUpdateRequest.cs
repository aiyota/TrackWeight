namespace TrackWeight.Api.Contracts;

public record UserUpdateRequest(
    Guid Id,
    string? UserName,
    string? FirstName,
    string? LastName,
    string? Password);
