﻿namespace TrackWeight.Api.Contracts;

public class UserLoginRequest
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
