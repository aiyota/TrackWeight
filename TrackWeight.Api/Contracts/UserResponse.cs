namespace TrackWeight.Api.Contracts;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email);
