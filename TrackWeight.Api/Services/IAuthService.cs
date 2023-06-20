using TrackWeight.Api.Models;

namespace TrackWeight.Api.Services;

public interface IAuthService
{
    string GenerateUserToken(User user);
}
