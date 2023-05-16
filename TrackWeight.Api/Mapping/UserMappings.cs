using TrackWeight.Api.Contracts;
using TrackWeight.Api.Models;

namespace TrackWeight.Api.Mapping;

public static class UserMappings
{
    public static UserResponse DomainToResponse(this User user) =>
        new UserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email);
}
