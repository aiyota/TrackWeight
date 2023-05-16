namespace TrackWeight.Api.Contracts;

public record UserRegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password);
