namespace TrackWeight.Api.Contracts;

public record UserLoginRequest(
    string Email,
    string Password);
