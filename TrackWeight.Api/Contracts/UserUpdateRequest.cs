namespace TrackWeight.Api.Contracts;

public record UserUpdateRequest(
    string? Email,
    string? FirstName,
    string? LastName,
    string? Password);
