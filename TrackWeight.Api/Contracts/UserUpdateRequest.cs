namespace TrackWeight.Api.Contracts;

public record UserUpdateRequest(
    Guid Id,
    string? Email,
    string? FirstName,
    string? LastName,
    string? Password);
