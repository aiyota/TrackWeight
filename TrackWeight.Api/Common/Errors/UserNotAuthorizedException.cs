using System.Net;

namespace TrackWeight.Api.Common.Errors;

public class UserNotAuthorizedException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public string ErrorMessage => "User is not authorized to make this change";
}
